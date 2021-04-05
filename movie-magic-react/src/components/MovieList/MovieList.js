import React from 'react'
import { Link } from 'react-router-dom';
import Card from '../controls/Card';
import Spinner from '../controls/Spinner';
import useMovieList from './MovieListHook'

const MovieList = () => {

    const { movieList, isLoading } = useMovieList();

    return (
        <>
            {
                isLoading && <Spinner />
            }
            {
                !isLoading && movieList != null && 
                <div className="wrapper">
                    <div className="cards_wrap">
                        {
                            movieList && movieList.map((movie) => (
                                <Link to={`/movie/${movie.id}`} key={movie.id} style={{textDecoration: 'none'}}>
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
