import React, { useState, useEffect } from 'react'
import { getMovieList } from '../../service/MovieListService';

function useMovieList() {
    const [movieList, setMovieList] = useState([]);
    const [error, setError] = useState();
    const [isLoading, setIsLoading] = useState();

    useEffect(() => {
        setIsLoading(true);
        getMovieList((res) => {
            const movieListData = res.data.moviesModel;
            setMovieList(movieListData);
            setError(null);
            setIsLoading(false);
        }, (err) => {
            if (err.response !== undefined &&
                err.response !== null && err.response.data !== null && err.response.data !== undefined) {
                setError(`Error Code: ${err.response.data.ErrorGuid} - Message: ${err.response.data.ErrorMessage}`);
            } else {
                setError(`Unable to retrieve data.`)
            }
            setIsLoading(false);
        })
    }, [])

    return {
        movieList, error, isLoading
    }
}

export default useMovieList;