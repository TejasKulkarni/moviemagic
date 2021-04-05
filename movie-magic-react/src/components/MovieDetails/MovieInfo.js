import React from 'react'

const MovieInfo = ({title, year, rating, rated, runtime, genre, released, votes}) => {
    return (
        <>
            <div className="movie-details-links">
                <ul>
                    <li><a href="#">Full Cast </a></li>
                    <li><a href="#">Director </a></li>
                    <li><a href="#">Writer </a></li>
                    <li><a href="#">Ratings </a></li>
                    <li><a href="#">Language </a></li>
                    <li><a href="#">Country </a></li>
                </ul>
            </div>
            <div className="md-title-container">
                <div>
                    <span className="md-title">{title}</span>
                    <span className="md-year">({year})</span>
                </div>
                <div className="md-rating">
                    <div className="md-rating-icon">
                        <svg aria-hidden="true" focusable="false" data-prefix="fas" data-icon="star" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" className="svg-inline--fa fa-star fa-w-18">
                            <path fill="currentColor" d="M259.3 17.8L194 150.2 47.9 171.5c-26.2 3.8-36.7 36.1-17.7 54.6l105.7 103-25 145.5c-4.5 26.3 23.2 46 46.4 33.7L288 439.6l130.7 68.7c23.2 12.2 50.9-7.4 46.4-33.7l-25-145.5 105.7-103c19-18.5 8.5-50.8-17.7-54.6L382 150.2 316.7 17.8c-11.7-23.6-45.6-23.9-57.4 0z" className=""></path>
                        </svg>
                    </div>
                    <div className="md-rating-value">
                        <div>{rating} <span>/ 10</span> </div>
                    </div>
                </div>
            </div>
            <div className="md-movie-info-section">
                <div className="md-movie-info">
                    <span>{rated}</span>
                    <span>{runtime}</span>
                    <span>{genre}</span>
                    <span>{released}</span>
                </div>
                <div className="md-movie-votes">
                    <span>{votes}</span>
                </div>
            </div>
        </>
    )
}

export default MovieInfo
