import { BrowserRouter, Route, Switch } from 'react-router-dom';

import Footer from './components/Footer/Footer';
import Header from './components/Header/Header';
import MovieList from './components/MovieList/MovieList';
import MovieDetails from './components/MovieDetails/MovieDetails';

import './App.css';

function App() {
  return (
    <BrowserRouter>
      <div className="md-main">
        <Header />
        <Switch>
          <Route exact path="/">
            <MovieList />
          </Route>
          <Route path="/movie/:source/:id">
            <MovieDetails />
          </Route>
        </Switch>
        <Footer />
      </div>
    </BrowserRouter>
  );
}

export default App;
