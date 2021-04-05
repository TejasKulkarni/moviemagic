import React from 'react'
import { splitStringIntoArray } from '../../helpers/stringHelper'

const Cast = ({actors}) => {

    const actorsArray = splitStringIntoArray(actors);

    return (
        <>
            <h2>Cast</h2>
            <h3>Cast overview</h3>
            <div className="md-cast-container">
                {actorsArray && actorsArray.map((actor, index) => (
                    <div key={index} className="md-cast-row">
                        <img src="https://via.placeholder.com/30" alt="actor_image" />
                        <span>{actor}</span>
                    </div>
                ))
                }
            </div>
        </>
    )
}

export default Cast
