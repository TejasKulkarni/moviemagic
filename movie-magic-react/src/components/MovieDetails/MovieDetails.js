import React from 'react'
import Cast from './Cast';
import MovieInfo from './MovieInfo';
import Plot from './Plot';
import useMovieDetail from './MovieDetailsHook'
import Spinner from '../controls/Spinner';
import Error from '../Error/Error';
import Price from './Price';

const MovieDetails = () => {
    const { movieDetail, isLoading, error } = useMovieDetail();

    return (
        <>
            {isLoading && <Spinner />}
            {error && <Error errorMessage={error} />}
            {!isLoading && !error && movieDetail != null && movieDetail.movieDetails != null &&
                <div className="md-container">
                    <div className="movie-poster">
                        <div style={{backgroundImage: `URL(${movieDetail.movieDetails.poster})`}}></div>
                    </div>
                    <div className="md-info">
                        <MovieInfo {...movieDetail} />
                    </div>
                    <div className="md-movie-creators">
                        <h2>Movie creators</h2>
                        <div><span>Director:</span> {movieDetail.movieDetails.director}</div>
                        <div><span>Writers:</span>{movieDetail.movieDetails.writer}</div>
                    </div>
                    <div className="md-cast">
                        <Cast {...movieDetail}  />
                    </div>
                    <div className="md-plot">
                        <Plot {...movieDetail}  />
                    </div>
                    <div className="md-plot">
                        <Price cinemaWorldPrice={movieDetail.cinemaWorldPrice} filmWorldPrice={movieDetail.filmWorldPrice}  />
                    </div>
                </div>
            }
        </>
    )
}

export default MovieDetails
