'use strict';

var boardSize = 10; 
var delayMs = 1000; // delay between displays (ticks)

var generations = 0; // counter of generations (ticks)

// create array of 2 boards
var boards = new Array(2);
// point to current board
var currentBoard = 0; // (0 or 1)
var previousBoard = 1; // (1 or 0)
// create empty boards, defaulted to all zeroes
boards[0] = matrix(boardSize, boardSize, 0);
boards[1] = matrix(boardSize, boardSize, 0);

var App = angular.module('myApp', ['ngSanitize','$http']);

angular.module('myApp', ['ngSanitize'])
    .controller('AppCtrl', ['$scope','$http', function ($scope,$http) {
        var show = false;
        $scope.boardSize = boardSize;
        $scope.delayMs = delayMs;
        $scope.boardHTML = "";
        $scope.message = "";

        $scope.on = function ($sce) {
            if ($scope.counting === true) return false;
            $scope.message = "";
            $scope.generations = 1;
            $scope.counting = true;
            $scope.intervalInstance = setInterval($scope.updateDisplayUsingAPI, 1000);
            // fill intial board with random values
            boards[currentBoard] = randomFillBoard(currentBoard, boardSize, boardSize);
            $scope.boardHTML = getBoardHTML(currentBoard, boardSize, boardSize);
            $scope.currentBoard = currentBoard;
            show = true;
        }

        $scope.off = function () {
            show = false;
            clearInterval($scope.intervalInstance);
            alert("Stopping! \nClick START to go again.");
        }

        $scope.updateDisplay = function () {
            $scope.generations++;

            // get new board
            // -- save currentBoard number, get new currentBoard number
            previousBoard = currentBoard;
            currentBoard = 1 - currentBoard;
            $scope.currentBoard = currentBoard;
            // now we are pointing to board to be filled
            boards[currentBoard] = generateNewBoard(previousBoard, boardSize, boardSize);
            $scope.boardHTML = getBoardHTML(currentBoard, boardSize, boardSize);

            $scope.counting = false;
            $scope.$apply();
            // compare to previous board
            // if SAME, STOP with special message
            if (boardsTheSame(currentBoard, previousBoard, boardSize, boardSize)) {
                $scope.generations++;
                clearInterval($scope.intervalInstance);
                show = false;
                alert("HEY! NEXT BOARD IS THE SAME!\nClick STOP.\n");
                $scope.$apply();
            }
        }

        class Cell {
            constructor(isAlive) {
                this.IsAlive= isAlive;
             
            }
        }; 

        

        $scope.updateDisplayUsingAPI= function () {
            $scope.generations++;

            // get new board
            // -- save currentBoard number, get new currentBoard number
            previousBoard = currentBoard;
            currentBoard = 1 - currentBoard;
            $scope.currentBoard = currentBoard;
            // now we are pointing to board to be filled

            var Row = {
            
            };

            class  Cell 
            {
                    constructor(isAlive)
                    {
                        this.IsAlive = isAlive; 
                    }

                    getLife(     )
                    {
                        return this.IsAlive; 

                    }

                    setLife(isAlive)
                    {
                        this.IsAlive=isAlive; 
                    }
            }
            var CurrentBoard  = {
                RowCount:10, 
                ColumnCount:10, 
                Rows:[] 

            };
            CurrentBoard.Rows.push(boards[0]);

            var req = {
                method: 'POST',
                url: 'http://localhost:50013/api/gameoflife/CreateNewBoard',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: CurrentBoard
            }

            $http(req).then(function (response) {
               //response.data
            }, function () {


            });


            boards[currentBoard] = generateNewBoard(previousBoard, boardSize, boardSize);
            $scope.boardHTML = getBoardHTML(currentBoard, boardSize, boardSize);

            $scope.counting = false;
            $scope.$apply();
            // compare to previous board
            // if SAME, STOP with special message
            if (boardsTheSame(currentBoard, previousBoard, boardSize, boardSize)) {
                $scope.generations++;
                clearInterval($scope.intervalInstance);
                show = false;
                alert("HEY! NEXT BOARD IS THE SAME!\nClick STOP.\n");
                $scope.apply();
            }
        }
        $scope.showButton = function () {
            return show;
        }
    }]);

function matrix(rows, cols, defaultValue) {
    var arr = [];
    // Creates all lines:
    for (var i = 0; i < rows; i++) {
        // Creates an empty line
        arr.push([]);
        // Adds cols to the empty line:
        arr[i].push(new Array(cols));
        for (var j = 0; j < cols; j++) {
            // Initializes:
            arr[i][j] = defaultValue;
        }
    }
    return arr;
}

function randomFillBoard(boardnum, rows, cols) {
    var arr = boards[boardnum];
    for (var i = 0; i < rows; i++) {
        for (var j = 0; j < cols; j++) {
            // Initializes:
            arr[i][j] = getRandBinary();
        }
    }
    return arr;
}

function generateNewBoard(prevboardnum, rows, cols) {

    var arr = matrix(boardSize, boardSize, 0);
    for (var i = 0; i < rows; i++) {
        for (var j = 0; j < cols; j++) {
            // Set each cell
            arr[i][j] = getNewLifeValue(prevboardnum, i, j, rows, cols);
        }
    }
    return arr;

}

function getNewLifeValue(prevboardnum, row, col, rows, cols) {
    // save current life value
    var startLifeValue = boards[prevboardnum][row][col];
    var newLifeValue = 0;

    // check all surrounding squares
    var startrow = (row > 0) ? (row - 1) : row;
    var endrow = (row < (rows - 1)) ? (row + 1) : row;
    var startcol = (col > 0) ? (col - 1) : col;
    var endcol = (col < (cols - 1)) ? (col + 1) : col;
    var liveNeighbors = 0;
    for (var i = startrow; i <= endrow; i++) {
        for (var j = startcol; j <= endcol; j++) {
            // skip the current space itself
            if ((i == row) && (j == col)) {
                // skip
            } else {
                liveNeighbors += boards[prevboardnum][i][j];
            }
        }
    }

    if (startLifeValue == 0) {
        // if currently DEAD, only ONE check
        newLifeValue = 0;
        if (liveNeighbors == 3) newLifeValue = 1;
    } else {
        // WAS alive; only lives on if 2 or 3 live neighbors
        newLifeValue = 0;
        if ((liveNeighbors == 2) || (liveNeighbors == 3)) newLifeValue = 1;
    }

    return newLifeValue;
}


function getRandBinary() {
    return Math.floor(Math.random() * 2);
}

function boardsTheSame(current, previous, rows, cols) {
    var diffs = 0;

    for (var i = 0; i < rows; i++) {
        for (var j = 0; j < cols; j++) {
            if (boards[current][i][j] != boards[previous][i][j]) {
                diffs++;
            }
        }
    }
    return (diffs == 0) ? true : false;
}


function getBoardHTML(boardnum, rows, cols) {
    var boardHTML = "";
    var arr = boards[boardnum];

    for (var i = 0; i < rows; i++) {
        for (var j = 0; j < cols; j++) {
            var code = "<span class='cell ";
            code += (arr[i][j] == 0) ? "dead" : "alive";
            code += "'></span>";
            boardHTML += code;
        }
        boardHTML += "<br/>\n";
    }
    return boardHTML;
}