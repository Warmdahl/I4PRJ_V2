import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FindBorger } from './components/FindBorger';
import { BoligListe } from './components/BoligListe';
import { BorgerVisning } from './components/BorgerVisning';
import { NyRapport } from './components/NyRapport';
import { OpdaterBorger } from './components/OpdaterBorger';
import { OpretBorger } from './components/OpretBorger';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
       
        <Route path='/FindBorger' component={FindBorger} />
        <Route path='/BoligListe' component={BoligListe} />
        <Route path='/BorgerVisning' component={BorgerVisning}/>
        <Route path='/NyRapport' component={NyRapport}/>
        <Route path='/OpdaterBorger' component={OpdaterBorger} />
        <Route path='/OpretBorger' component={OpretBorger} />
      </Layout>
    );
  }
}
