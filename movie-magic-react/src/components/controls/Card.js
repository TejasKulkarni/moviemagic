import React from 'react';

const Card = ({ poster, title, year, type }) => {
    const onError = (e) => {
        e.target.src = "https://via.placeholder.com/300"
    }

    return (
        <div className="card_item">
            <div className="card_inner">
                <div className="card_top">
                    <img className="card-image" src={poster} alt="car" onError={onError} />
                </div>
                <div className="card_bottom">
                    <div className="card_info">
                        <p className="title">{title}</p>
                    </div>
                    <div className="card_category">
                        <div className="row">
                            <div className="column">
                                <span className="text-mute">TYPE</span>
                            </div>
                            <div className="column">
                                <span className="text-mute">RELEASE DATE</span>
                            </div>
                        </div>
                        <div className="row">
                            <div className="column">
                                <span className="text-highlight">{type}</span>
                            </div>
                            <div className="column">
                                <span className="text-highlight">{year}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Card;
