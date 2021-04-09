import React from 'react'
import { Link } from 'react-router-dom';

import Error from '../Error/Error';
import Card from '../controls/Card';
import Spinner from '../controls/Spinner';
import useMovieList from './MovieListHook'

const MovieList = () => {

    const { movieList, isLoading, error } = useMovieList();

    return (
        <>
            {isLoading && <Spinner />}
            {error && <Error errorMessage={error} />}
            {
                !isLoading && !error && movieList != null && 
                <div className="wrapper">
                    <div className="cards_wrap">
                        {
                            movieList && movieList.map((movie) => (
                                <Link to={`/movie/${movie.source}/${movie.id}`} key={movie.id} style={{textDecoration: 'none'}}>
                                    <Card poster={movie.poster}
                                        title={movie.title} type={movie.type} year={movie.year} />
                                </Link>
                            ))
                        }
                    </div>
                </div>
            }
        </>
    )
}

export default MovieList
