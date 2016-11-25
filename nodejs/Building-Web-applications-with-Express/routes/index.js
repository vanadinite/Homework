'use strict';

const applyUsersRouter = require('./users-router');
const applyHomeRouter = require('./home-router');
const applyAccountRouter = require('./account-router');
const applySuperheroRouter = require('./superhero-router');

module.exports = function (app, data) {
  applyUsersRouter(app, data);
  applyHomeRouter(app);
  applyAccountRouter(app, data.userData);
  applySuperheroRouter(app, data.superheroesData);

  app.get('*', (req, res) => res.redirect('/'));
};