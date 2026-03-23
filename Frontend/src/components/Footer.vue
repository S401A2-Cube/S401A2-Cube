<script setup>
defineProps({
    currentYear: { type: Number, default: new Date().getFullYear() }
});
</script>

<template>
    <footer id="footer">
        <div class="footer-container">
            <div class="footer-section">
                <h3>About CUBE</h3>
                <ul>
                    <li><a href="#">Company</a></li>
                    <li><a href="#">Careers</a></li>
                    <li><a href="#">Press</a></li>
                    <li><a href="#">Sustainability</a></li>
                </ul>
            </div>

            <div class="footer-section">
                <h3>Products</h3>
                <ul>
                    <li><a href="#">Bikes</a></li>
                    <li><a href="#">Accessories</a></li>
                    <li><a href="#">Components</a></li>
                    <li><a href="#">Apparel</a></li>
                </ul>
            </div>

            <div class="footer-section">
                <h3>Support</h3>
                <ul>
                    <li><a href="#">Dealer Locator</a></li>
                    <li><a href="#">FAQ</a></li>
                    <li><a href="#">Contact</a></li>
                    <li><a href="#">Warranty</a></li>
                </ul>
            </div>

            <div class="footer-section">
                <h3>Legal</h3>
                <ul>
                    <li><a href="#">Privacy Policy</a></li>
                    <li><a href="#">Terms & Conditions</a></li>
                    <li><a href="#">Cookie Policy</a></li>
                    <li><a href="#">Imprint</a></li>
                </ul>
            </div>

            <div class="footer-section social">
                <h3>Follow Us</h3>
                <div class="social-links">
                    todo
                </div>
            </div>
        </div>

        <div class="footer-bottom">
            <p>&copy; {{ currentYear }} CUBE. All rights reserved.</p>
        </div>
    </footer>
</template>

<style scoped>
#footer {
    background-color: #1a1a1a;
    color: #fff;
    padding: 4rem 2rem 2rem;
    margin-top: 6rem;
}

.footer-container {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    gap: 3rem;
    max-width: 1200px;
    margin: 0 auto 3rem;
}

.footer-section h3 {
    font-size: 14px;
    text-transform: uppercase;
    letter-spacing: 1px;
    margin-bottom: 1.5rem;
    font-weight: 600;
}

.footer-section ul {
    list-style: none;
    padding: 0;
    margin: 0;
}

.footer-section li {
    margin-bottom: 0.8rem;
}

.footer-section a {
    color: #ccc;
    text-decoration: none;
    font-size: 14px;
    transition: color 0.3s ease;
}

.footer-section a:hover {
    color: #fff;
}

.social-links {
    display: flex;
    gap: 1.5rem;
}

.social-links a {
    font-size: 18px;
    display: inline-block;
}

.footer-bottom {
    border-top: 1px solid #333;
    padding-top: 2rem;
    text-align: center;
    font-size: 12px;
    color: #999;
}
</style>    