import React from 'react'

const Footer = () => {
    return (
        <footer>
            <div className="footer-container">
                <div className="footer-icons">
                    <span className="fab fa-facebook-f"></span>
                    <span className="fab fa-twitter"></span>
                    <span className="fab fa-twitch"></span>
                    <span className="fab fa-instagram"></span>
                    <span className="fab fa-youtube"></span>
                </div>
                <div className="footer-labels">
                    <span>Get the App</span>
                    <span>Help</span>
                    <span>Site Index</span>
                    <span>Pro</span>
                    <span>Box Office News</span>
                    <span>Developers</span>
                </div>
                <div className="footer-labels">
                    <span>Press Room</span>
                    <span>Advertising</span>
                    <span>Jobs</span>
                    <span>Condition of use</span>
                    <span>Privacy Policy</span>
                    <span>Internet-Based Ads</span>
                </div>
            </div>
        </footer>
    )
}

export default Footer
