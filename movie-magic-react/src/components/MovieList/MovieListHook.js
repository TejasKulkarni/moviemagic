import React, { useState, useEffect } from 'react'
import { getMovieList } from '../../service/MovieListService';

function useMovieList() {
    const [movieList, setMovieList] = useState([]);
    const [error, setError] = useState();
    const [isLoading, setIsLoading] = useState();

    useEffect(() => {
        setIsLoading(true);
        getMovieList((res) => {
            const movieListData = res.data.movies;
            setMovieList(movieListData);
            setError(null);
            setIsLoading(false);
        }, (err) => {
            if (err.response !== null && err.response.data !== null) {
                setError(`Error Code: ${err.response.data.ErrorGuid} - Message: ${err.response.data.ErrorMessage}`);
            }
            setIsLoading(false);
        })
    }, [])

    return {
        movieList, error, isLoading
    }
}

export default useMovieList;