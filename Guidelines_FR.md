# Guide du Projet CUBE

J'ai rédigé ce document pour résumer tout ce que nous devons savoir pour commencer. Le but est de garder notre code propre, d'éviter les énormes conflits de fusion (merge conflicts) et de nous assurer de pouvoir travailler sur ce projet ensemble de manière efficace.

## Table des matières
1. [Pour commencer](#pour-commencer)
2. [Workflow Git et GitHub](#workflow-git-et-github)
3. [Structure et architecture du projet](#structure-et-architecture-du-projet)
4. [Normes de code et syntaxe](#normes-de-code-et-syntaxe)
5. [Garder le code à jour](#garder-le-code-à-jour)
6. [Conventions pour les branches et les commits](#conventions-pour-les-branches-et-les-commits)

## Pour commencer

Vous ne devriez avoir à faire cette configuration qu'une seule fois. 

Si vous êtes sur Windows, vous pouvez installer les outils nécessaires en ouvrant PowerShell et en exécutant : <br>
`winget install Git.Git Microsoft.DotNet.SDK.8 Node.js`

Après cela, vous pouvez cloner le dépôt sur votre machine locale :
`git clone <lien_du_depot>`

**Configuration du Frontend (Vue.js) :**
1. Allez dans le dossier frontend : `cd Frontend`
2. Installez les paquets Node requis : `npm install`
3. Lancez le serveur de développement local : `npm run dev`

**Configuration du Backend (API .NET) :**
1. Ouvrez un nouveau terminal et allez dans le dossier backend : `cd Backend`
2. Lancez le serveur d'API local : `dotnet run`

## Workflow Git et GitHub

> [!CAUTION]
> S'il vous plaît, ne poussez pas de code directement sur la branche `main`. Créez toujours une nouvelle branche pour votre fonctionnalité spécifique afin que nous puissions examiner le code en toute sécurité avant qu'il ne soit en ligne.

Pour chaque fonctionnalité que vous souhaitez ajouter (listée sur notre tableau Trello), veuillez suivre ces étapes :

### 1) Créer une nouvelle branche
Créez et basculez sur une nouvelle branche pour votre tâche. Par exemple :<br>
`git checkout -b feature/implementation-navbar`

### 2) Faire vos modifications
Ouvrez le projet dans votre éditeur de code et développez votre fonctionnalité.

### 3) Sauvegarder vos modifications sur Git et GitHub
Indexez vos fichiers (stage) et commitez-les avec un message clair : <br>
`git add .` <br>
`git commit -m "Implémenter une barre de navigation sur tout le site"` <br>

Ensuite, poussez votre branche spécifique vers le dépôt distant : <br>
`git push origin feature/implementation-navbar`

### 4) Créer une Pull Request (PR)
Une fois que votre branche est finalisée et fonctionne localement, allez sur la page du dépôt sur GitHub.
1. Vous verrez un gros bouton vert indiquant "Compare & pull request". Cliquez dessus.
2. Ajoutez une brève description de ce que vous avez modifié.
3. Cliquez sur "Create pull request".

## Structure et architecture du projet

Le projet est divisé en deux parties distinctes : le frontend et le backend. Chacune d'elles est contenue dans son propre dossier.
* `Frontend/` contient le code Vue.js.
* `Backend/` contient l'API C# .NET.

Le backend se déploiera automatiquement sur le serveur distant chaque fois qu'un nouveau commit ou une fusion a lieu sur la branche principale.

> [!IMPORTANT]
> **Règle pour la base de données :** Nous développons une API code-first. Si vous modifiez le modèle C#, la base de données doit également migrer. Assurez-vous d'envoyer un message à tout le monde sur Discord avant d'exécuter `dotnet ef migrations add`. Si plusieurs personnes travaillent sur des migrations en même temps, la base de données va casser. Pour éviter cela, nous allons nous coordonner et faire une seule migration à la fois, uniquement depuis la branche `main`.

### À propos du Frontend
Chaque fois que vous récupérez (pull) du nouveau code, vous pourriez avoir besoin d'installer des dépendances de projet mises à jour. Par précaution, il est utile de lancer :
`npm install`

## Normes de code et syntaxe

> [!IMPORTANT]
> Nous devons suivre cette syntaxe pour que notre code reste propre et lisible pour tout le monde. Les PR qui ne respectent pas ce formatage devront être révisées avant d'être fusionnées, et je n'ai vraiment pas envie que quelqu'un doive refaire tout son travail !

### Espacement et indentation
* Assurez-vous que votre code est correctement indenté.
* L'indentation doit toujours être de 2 espaces. Pas de tabulations.
* Essayez d'éviter d'ajouter plus d'une ligne vide. Gardons le code compact mais lisible.

### Conventions de nommage et accolades
Nous adoptons ces principes pour garder le code entièrement uniforme et prévisible pour toute l'équipe. 

* **Accolades style Allman :** Chaque accolade ouvrante `{` et fermante `}` doit se trouver sur sa propre ligne dédiée. Cela crée un bloc de code visuellement distinct et aligne les accolades verticalement. 
* **camelCase :** La première lettre du premier mot est en minuscule, et la première lettre de chaque mot suivant est en majuscule (par exemple, `maVariableLocale`). Nous l'utilisons pour les variables locales, les paramètres de fonction et les champs privés.
* **PascalCase :** La première lettre de chaque mot est en majuscule, y compris le tout premier mot (par exemple, `MaClassePublique`). Nous l'utilisons pour les classes, les méthodes et les propriétés publiques.

### Exemples
Voici un exemple complet en C# :

```c#
using System;

namespace CodingStandards
{
  public class EmployeeRecord
  {
    private string _employeeName;  // les variables privées commencent par un tiret bas et utilisent le camelCase
    public int EmployeeId { get; set; } // les variables publiques utilisent le PascalCase
    
    public EmployeeRecord(string name, int id)
    {
        _employeeName = name;
        EmployeeId = id;
    }
    
    // Les paramètres longs passent à la ligne suivante et s'alignent pour la lisibilité
    public void PrintDetailedReport(string reportTitle, string departmentName, bool includeInactive)
    { // Accolades style Allman PARTOUT.
      bool isActive = true;  // les variables locales utilisent aussi le camelCase
      
      if (isActive)
      {
        // Les expressions ou appels de méthodes très longs passent également à la ligne suivante
        string output = string.Format(
          "Name: {0}, ID: {1}, Dept: {2}, Title: {3}",
          _employeeName,
          EmployeeId,
          departmentName,
          reportTitle);
          
        Console.WriteLine(output);
      }
    }
  }
}

```

Et un exemple en JS :

```html
<script setup>
import { ref } from 'vue';

// les variables locales utilisent le camelCase
const employeeName = ref('Jane Doe');
const employeeId = ref(101);

// Accolades style Allman PARTOUT
const printDetailedReport = (reportTitle, departmentName, includeInactive) =>
{
  if (employeeId.value > 0)
  {
    // Les expressions ou appels de méthodes très longs passent également à la ligne suivante
    console.log(
      "Name: " + employeeName.value,
      "ID: " + employeeId.value,
      "Dept: " + departmentName,
      "Title: " + reportTitle
    );
  }
};

const getEmployeeData = () =>
{
  // Une petite limite de JavaScript à noter :
  // Vous ne pouvez pas utiliser une accolade Allman directement après une instruction 'return'.
  // JavaScript insérera automatiquement un point-virgule après 'return', ce qui cassera le code.
  // L'accolade ouvrante d'un objet retourné doit rester sur la même ligne.
  return {
    fullName: employeeName.value,
    idNumber: employeeId.value,
    status: 'Active'
  };
};
</script>

<template>
  <div class="employee-card">
    <h2>{{ employeeName }}</h2>
    
    <button
      class="btn-primary"
      type="button"
      @click="printDetailedReport('Rapport Q3', 'Ingénierie', false)"
    >
      Imprimer les données
    </button>
  </div>
</template>

```

## Garder le code à jour

Pendant que vous travaillez, d'autres coéquipiers verront leurs PR fusionnées dans main. Vous devez régulièrement importer ces mises à jour dans votre propre branche pour ne pas prendre de retard.
Cette étape est super importante. Chaque fois que vous commencez à coder, et chaque fois que vous finissez d'implémenter une fonctionnalité, assurez-vous de faire ceci :

1. Revenir sur la branche principale : `git checkout main`
2. Télécharger les dernières mises à jour : `git pull origin main`
3. Revenir sur votre branche de travail : `git checkout feature/nom-de-votre-tache`
4. Fusionner les nouvelles mises à jour de main dans votre branche : `git merge main`

## Conventions pour les branches et les commits

Pour garder notre dépôt organisé, tenons-nous à ces conventions de nommage.

### Noms des branches

Veuillez préfixer le nom de votre branche avec le type de travail que vous effectuez, suivi d'une brève description séparée par des tirets.

* `feature/` pour les nouveaux ajouts (`feature/systeme-de-connexion`)
* `bugfix/` pour corriger des problèmes (`bugfix/alignement-navbar`)
* `tests/` pour écrire des tests unitaires ou moq (`tests/service-connexion`)
* `docs/` pour rédiger ou mettre à jour la documentation (`docs/mise-a-jour-readme`)

### Messages de commit

Rédigez vos messages de commit à l'impératif. Voyez cela comme si vous donniez un ordre au code.

* **Bon :** `Ajouter la logique backend d'authentification utilisateur`
* **Bon :** `Corriger le lien d'image cassé sur la page d'accueil`
* **Mauvais :** `Ajout de l'authentification utilisateur`
* **Mauvais :** `Correction des images`
