'use strict';
var express = require('express');
var https = require('https');
var router = express.Router();

class Teamer {

    constructor(response) {
        this.response = response;
    }

    getTeams(year,countMatch) {
        this.teams = [];
        this.year = year;
        this.maxCountMatches = countMatch;
        let page = 1;
        let countPage = 100;
        //https://jsonmock.hackerrank.com/api/football_matches?competition=UEFA%20Champions%20League&year=2000&page=2
        let promises = [];
        let context = this;
        while (page <= countPage) {
            //while all pages will be load
            promises.push(new Promise((resolve, reject) => {
                var apiUrl = 'https://jsonmock.hackerrank.com/api/football_matches?competition=UEFA%20Champions%20League';
                https.get(`${apiUrl}&year=${year}&page=${page}`, (resp) => {
                    let data = '';
                    resp.on('data', (chunk) => {
                        data += chunk;
                    });

                    // The whole response has been received. Print out the result.
                    resp.on('end', () => {
                        let datas = JSON.parse(data);
                        resolve(datas);
                    });
                });
            })/*.then((datas) => {
                for (let team of datas.data) {
                    this.addTeam(team.team1);
                    this.addTeam(team.team2);
                }
                //resolve();
            })*/);
            page++;
        }
        Promise.all(promises).then(results => {
            let matches = [];
            for (let datas of results) {
                for (let team of datas.data) {
                    //matches.push({ team1: team.team1, team2: team.team2});
                    this.addTeam(team.team1);
                    this.addTeam(team.team2);
                }
            }
            //let res = this.teams.sort((a, b) => a.count - b.count);
            let res = this.teams.filter(x => x.count >= countMatch);
            res = res.sort((a, b) => a.count - b.count);
            let result = {
                year: this.year,
                maxCountMatches: this.maxCountMatches,
                teams: this.teams,
                resTeams: res
            }

            this.response.render('LeagueChampions',result);
            //this.response.send(matches.map(x => `${x.team1} - ${x.team2}`).join("<br/>"));
        });
    }

    getPage(year, page) {
        var apiUrl = 'https://jsonmock.hackerrank.com/api/football_matches?competition=UEFA%20Champions%20League';
        https.get(`${apiUrl}&year=${year}&page=${page}`, (resp) => {
            let data = '';
            resp.on('data', (chunk) => {
                data += chunk;
            });

            // The whole response has been received. Print out the result.
            resp.on('end', () => {
                let datas = JSON.parse(data);
                resolve(datas);
            });
        });
    }

    addTeam(teamName) {
        let teamFind = this.teams.filter(x => x.name == teamName);
        let team = undefined;

        if (teamFind.length < 1) {
            team = {
                name: teamName,
                count: 0
            }
            this.teams.push(team);
        }
        else {
            team = teamFind[0];
        }
        team.count++;
    }
}

/* GET teams listing. */
router.get('/', function (req, res) {

    let year = +req.query.year || undefined;
    let k = +req.query.k || undefined;

    if (year === undefined || k === undefined)
        res.send('Not available params(year,k).');

    let teamer = new Teamer(res);

    let teams = teamer.getTeams(year,k);
    
});

module.exports = router;