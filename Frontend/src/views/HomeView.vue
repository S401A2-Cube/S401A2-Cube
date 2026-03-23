<script setup>

</script>


<template>
    <main>
        <video class="video" autoplay="" playsinline="" muted="" loop="" controlslist="noplaybackrate nodownload" disablepictureinpicture=""> 
            <source src="@/assets/video/20260113_Nuroad_Klassik_Hybrid_loop_noaudio_16x9.mp4" type="video/mp4"> 
        </video>
        <div id="home_center">
            <p class="title">EXPLORE THE UNKNOWN</p>
            <p class="subtitle">GRAVEL OR E-GRAVEL. GO FARTHER.</p>
            <div class="hsplit center">
                <a class="link fitcontent">DISCOVER THE NUROAD SERIES</a>
                <a class="link fitcontent">DISCOVER THE NUROAD HYBRID SERIES</a>
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
    video {
        position: absolute;
        left: 0;
        top:0;
        width: 100vw;
        height: 100vh;
        z-index: -1;
        object-fit: cover;
        object-position: center;
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
    
    #home_center {
        width: 70vw;
        display: flex;
        flex-direction: column;
        gap: 1rem;
        color: white;
    }

    #home_center .link {
        border-bottom: 1px solid white;
    }

    #discover_section {
        width: 75vw;
        display: flex;
        flex-direction: column;
        gap: 4rem;
    }
</style>