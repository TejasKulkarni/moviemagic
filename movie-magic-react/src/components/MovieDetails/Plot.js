import React from 'react'

const Plot = ({ movieDetails }) => {

    const { plot } = movieDetails;

    return (
        <div>
            <h2>Plot</h2>
            <p> {plot}</p>
        </div>
    )
}

export default Plot
