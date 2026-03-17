<script setup>

</script>


<template>
    <main>
        <div id="home_center">
            <p class="title">EXPLORE THE UNKNOWN</p>
            <p>GRAVEL OR E-GRAVEL. GO FARTHER.</p>
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
    }
    .subtitle {
        font-size: 1.2rem;
        font-weight: bold;
    }
    .title {
        font-size: 2.5em;
        font-weight: bolder;
    }
    .center {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
    }
    
    #home_center {
        width: 70vw;
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