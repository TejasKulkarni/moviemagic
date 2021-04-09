import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import { getMovieDetailsById } from '../../service/MovieListService';

function useMovieDetails() {
    const [movieDetail, setMovieDetail] = useState([]);
    const [error, setError] = useState();
    const [isLoading, setIsLoading] = useState();

    const { id, source } = useParams();

    useEffect(() => {
        setIsLoading(true);
        if (source !== null && (id !== null || id !== "0")) {
            getMovieDetailsById(id, source, (res) => {
                if (res.data !== null && res.data !== "") {
                    const movieDetailsData = res.data;
                    setMovieDetail(movieDetailsData);
                    setError(null);
                }
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
        }
    }, [])

    return {
        movieDetail, error, isLoading
    }
}

export default useMovieDetails;