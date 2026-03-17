<script setup>

</script>


<template>
    <main>
        <div id="home_center">
            <p class="title">EXPLORE THE UNKNOWN</p>
            <p class="subtitle">GRAVEL OR E-GRAVEL. GO FARTHER.</p>
            <div class="hsplit">
                <a class="link">DISCOVER THE NUROAD SERIES</a>
                <a class="link">DISCOVER THE NUROAD HYBRID SERIES</a>
            </div>
        </div>
    </main>
    <main>
        <div id="discover_section">
            <div class="hsplit">
                <section class="vsplit">
                    <img src="@/assets/image/260218_Startseite_ACSLaunch_Feb2026_Gravel_1_800x800.jpg">
                    <div class="hsplit">
                        <img src="@/assets/image/260218_Startseite_ACSLaunch_Feb2026_Gravel_2_800x800.jpg">
                        <img src="@/assets/image/260218_Startseite_ACSLaunch_Feb2026_Gravel_3_800x800.jpg">
                    </div>
                </section>
                <section class="center">
                    <p class="subtitle">DISCOVER OUR NEW GRAVEL COLLECTION</p>
                    <p>ONE RIDE. MANY ROADS.</p>
                    <a class="link">VIEW GEAR</a>
                    <a class="link">VIEW HELMETS</a>
                </section>
            </div>
            <div class="hsplit">
                <section class="center">
                    <p class="subtitle">DISCOVER OUR MTB COLLECTION</p>
                    <p>TRAIL BY TRAIL.</p>
                    <a class="link">VIEW GEAR</a>
                    <a class="link">VIEW GOGGLES</a>
                    <a class="link">VIEW HELMETS</a>
                </section>
                <section class="vsplit">
                    <div class="hsplit">
                        <img src="@/assets/image/260218_Startseite_ACSLaunch_Feb2026_MTB_2_800x800.jpg">
                        <img src="@/assets/image/260218_Startseite_ACSLaunch_Feb2026_MTB_3_800x800.jpg">
                    </div>
                    <img src="@/assets/image/260218_Startseite_ACSLaunch_Feb2026_MTB_mobil_800x800.jpg">
                </section>
            </div>
        </div>
    </main>
</template>
<style scoped>
    main {
        width: 100vw;
        min-height: 100vh;
        display: flex;
        justify-content: center;
        align-items: center;
        overflow: hidden;
    }
    
    .link {
        border-bottom: 1px solid black;
        cursor: pointer;
        text-decoration: none;
        font-weight: bold;
        text-transform: uppercase;
        width: auto;
    }
    
    .subtitle {
        font-size: clamp(1.5rem, 3vw, 2.5rem);
        font-weight: 800;
        text-transform: uppercase;
        line-height: 1.1;
        text-align: center;
    }
    
    .title {
        font-weight: 900;
        font-size: clamp(3.5rem, 8vw, 8rem);
        line-height: 0.9;
        text-transform: uppercase;
        margin-bottom: 0.5rem;
    }
    
    .center {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        gap: 1rem;
    }
    
    #home_center {
        width: 70vw;
        display: flex;
        flex-direction: column;
        gap: 1rem;
    }

    #discover_section {
        width: 75vw;
        display: flex;
        flex-direction: column;
        gap: 4rem;
    }
    
    img {
        height: 100%;
        width: 100%;
        object-fit: cover;
        display: block;
    }

    .hsplit {
        display: flex;
        flex-direction: row;
        width: 100%;
        height: 100%;
        gap: 1rem;
    }
    
    .hsplit > * {
        width: 50%;
    }
    
    .vsplit {
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;
        gap: 1rem;
    }

    .vsplit > * {
        height: 50%;
    }
</style>