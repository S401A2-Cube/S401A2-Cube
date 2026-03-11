# Les DTOs (Data Transfer Objects)

https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio#prevent-over-posting

## Qu'est-ce qu'un DTO ?

Un DTO (Data Transfer Object) est un objet simple conçu spécifiquement pour transporter des données entre différents processus ou couches d'une application.
Il agit comme un conteneur regroupant les informations strictement nécessaires pour une opération réseau. Cet objet est totalement indépendant de la structure interne de votre base de données. 
En pratique, vous créez une classe qui représente exactement le contrat de communication de votre API.

Les entités générées par Entity Framework Core gèrent la logique de la base de données et les relations complexes. 
Le DTO prend la forme d'une structure de données plate et sécurisée, prête à être sérialisée en JSON sans causer d'erreurs.

## Quand utiliser un DTO ?

Vous devez utiliser un DTO lorsque vous possédez un modèle de données complexe avec de nombreuses relations que vous ne souhaitez pas exposer directement au client. 
Le passage par un DTO crée une séparation nette entre le modèle de données interne et l'interface publique de l'API.

Un DTO est particulièrement utile pour :

- Empêcher le sur-postage (over-posting) : 
    Éviter qu'un utilisateur malveillant ne modifie des champs restreints (comme un rôle administrateur) en envoyant des données non sollicitées dans sa requête.

- Masquer des informations sensibles : 
    Cacher des propriétés que les clients ne sont pas censés voir, comme les mots de passe, les numéros de sécurité sociale ou les statuts internes.

- Réduire la charge réseau : 
    Omettre certaines propriétés lourdes pour alléger le poids de la réponse JSON.

- Éviter les boucles de sérialisation : 
    Prévenir les erreurs liées aux références circulaires causées par les propriétés de navigation bidirectionnelles d'Entity Framework.

### Exemple du problème (La boucle de sérialisation)

Imaginons une application gérant des films et des catégories. Vos entités Entity Framework ressemblent à ceci :

```csharp
public class Categorie
{
    public int Id { get; set; }
    public string Nom { get; set; }
    
    // Propriété de navigation vers les films de cette catégorie
    public virtual ICollection<Film> Films { get; set; }
}

public class Film
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string DescriptionSecrete { get; set; } // Donnée à masquer
    public int CategorieId { get; set; }
    
    // Propriété de navigation vers la catégorie du film
    public virtual Categorie CategorieNavigation { get; set; }
}

```

Si vous renvoyez directement un objet `Film` depuis votre contrôleur, le sérialiseur JSON va lire le film, puis sa `CategorieNavigation`. 
Dans cette catégorie, il va trouver une liste de `Films`, qu'il va lire, et chaque film aura à nouveau une `CategorieNavigation`. 
Cela crée une boucle infinie qui fait planter l'application.

### La solution avec un DTO

Pour résoudre ce problème, vous créez un `FilmDTO` qui "aplatit" les données et ne contient que l'essentiel :

```csharp
public class FilmDTO
{
    public int Id { get; set; }
    public string Nom { get; set; }
    // On récupère uniquement le nom de la catégorie, sans l'objet entier
    public string CategorieNom { get; set; } 
}

```

## Comment utiliser un DTO ?

L'utilisation d'un DTO implique de transformer vos données avant qu'elles ne soient envoyées au client. 
Voici les étapes d'implémentation standard illustrées avec du code :

1) Créer la classe DTO :     (Comme illustré dans l'étape précédente avec `FilmDTO`).

2) Récupérer l'entité source :    Interrogez votre base de données via le contexte Entity Framework.

3) Mapper les données :
    Transférez manuellement les valeurs de l'entité vers le DTO.

4) Retourner le DTO :
    Renvoyez l'objet simplifié depuis le contrôleur.

Voici comment cela se traduit dans un contrôleur d'API :

```csharp
[ApiController]
[Route("api/[controller]")]
public class FilmsController : ControllerBase
{
    private readonly FilmsDbContext _context;

    public FilmsController(FilmsDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmDTO>> GetFilm(int id) // utilisation de FilmDTO, au lieu de Film
    {
        // On projette directement les données dans le DTO depuis la requête
        var filmDTO = await _context.Films
            .Where(f => f.Id == id)
            .Select(f => new FilmDTO
            {
                Id = f.Id,
                Nom = f.Nom,
                CategorieNom = f.CategorieNavigation.Nom // EF Core fait la jointure SQL automatiquement
            })
            .FirstOrDefaultAsync();

        if (filmDTO == null)
        {
            return NotFound();
        }

        return filmDTO;
    }
}
```