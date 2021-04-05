import axios from 'axios';

export function getData(config, url, callback, errorcallback) {
  axios.get(url, config)
    .then(res => {
      if (callback != null) {
        callback(res);
      } else {
        return res.data;
      }
    })
    .catch(function (err) {
      if (errorcallback != null) {
        errorcallback(err);
      }
    })
}

export function postData(config, url, data, callback, errorcallback) {
  axios({
    method: "POST", url, data, headers: config
  }).then((res) => {
    if (callback) {
      callback(res.data);
    }
  }).catch(err => {
    if (errorcallback) {
      errorcallback(err);
    }
  });
}