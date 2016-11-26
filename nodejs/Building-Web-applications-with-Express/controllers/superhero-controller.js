'use strict';

module.exports = function (superheroesData, fractionsData, userData) {
  function index(req, res) {
    const page = +req.query.page || 0;
    const size = +req.query.size || 3;

    superheroesData.allWithPagination(page, size)
      .then(([superheroes, pageCount]) => {
        const pagination = {
          active: +pageCount > 1,
          pageSize: size,
          previous: {
            active: +page > 0,
            value: +page - 1
          },
          next: {
            active: +page < +pageCount - 1,
            value: +page + 1
          }
        };

        const isAuthenticated = req.isAuthenticated();
        res.render('./superheroes/index', {
          result: {
            superheroes,
            isAuthenticated,
            pagination
          }
        });
      })
      .catch((err) => {
        res.send(err);
      });
  }

  function createSuperhero(req, res) {
    const superhero = req.body;
    if (superhero.powers) {
      const powers = superhero.powers.split(/[,]+/).map(p => p.trim());
      superhero.powers = powers;
    }

    if (superhero.fractions) {
      const fractions = superhero.fractions.split(/[,]+/).map(f => f.trim());
      superhero.fractions = fractions;
    }

    superheroesData.createSuperhero(superhero)
      .then((createdSuperhero) => {
        if (createdSuperhero.fractions && createdSuperhero.fractions.length > 0) {
          createdSuperhero.fractions.forEach(f => {
            return fractionsData.findByName(f)
              .then((fraction) => {
                if (fraction) {
                  fraction.planets.push(createdSuperhero.planet);
                  return fractionsData.updateFractionPlanets(fraction);
                }

                return fractionsData.createFraction({
                  name: f,
                  alignment: createdSuperhero.alignment
                });
              })
              .catch((err) => {
                console.log(err);
              });
          });
        }
      })
      .then(() => {
        res.redirect('/superheroes');
      })
      .catch((err) => {
        res.send(err);
      });
  }

  function details(req, res) {
    if (!req.isAuthenticated()) {
      return res.redirect('/account/login');
    }

    return superheroesData.findById(req.params.superheroId)
      .then((superhero) => {
        res.render('./superheroes/details', {
          result: superhero
        });
      })
      .catch((err) => {
        res.send(err);
      });
  }

  function addSuperheroToFavorites(req, res) {
    if (!req.isAuthenticated()) {
      return res.redirect('/account/login');
    }

    const exists = req.user.favoriteHeroes.some(s => s._id === req.params.superheroId);
    if (exists) {
      return res.redirect('/account');
    }

    return superheroesData.findById(req.params.superheroId)
      .then((superhero) => {
        req.user.favoriteHeroes.push({
          name: superhero.name,
          _id: superhero._id
        });

        return userData.updateUser(req.user);
      })
      .then(() => {
        res.redirect('/account');
      })
      .catch((err) => {
        res.send(err);
      });
  }

  return {
    index,
    createSuperhero,
    addSuperheroToFavorites,
    details
  };
};