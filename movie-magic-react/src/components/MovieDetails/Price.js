import React from 'react'

const Price = ({ cinemaWorldPrice, filmWorldPrice }) => {
    return (
        <div>
            <div className="md-movie-creators">
                <h2>Price</h2>
                <div><span>Cinema World Ticket Price:</span> {cinemaWorldPrice === "" ? 'Price not available.' : cinemaWorldPrice}</div>
                <div><span>Film World Ticket Price:</span>{filmWorldPrice === "" ? 'No price available.' : filmWorldPrice}</div>
            </div>
        </div>
    )
}

export default Price
