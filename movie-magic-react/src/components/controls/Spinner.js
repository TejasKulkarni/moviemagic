import React from 'react';
import Loader from 'react-loader-spinner';

const Spinner = () => (
    <div className="mm-spinner">
        <Loader
            type="Circles"
            color="#dbb322"
            height={100}
            width={100}
        />
    </div>
)

export default Spinner;
