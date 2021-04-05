import React from 'react'
import { Link } from 'react-router-dom'

const Header = () => {
    return (
        <div className="nav-container">
            <nav>
                <Link to="/" style={{textDecoration: 'none'}}>
                    <div className="logo">MovieMagic</div>
                </Link>
                <form action="#">
                    <input type="search" className="search-data" placeholder="Search" />
                    <button type="submit" className="fa fa-search"></button>
                </form>
            </nav>
        </div>
    )
}

export default Header
