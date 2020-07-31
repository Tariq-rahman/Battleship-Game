// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/**
 *  Toggles the specified modal
 * param id: the id of the modal to toggle
 * param bool temp: (true )temporary toggle or (false) permanent toggle
 */
function toggleModal(id, temp) {
    var modal = document.getElementById(id);
    modal.classList.toggle("show-modal");

    if (temp) {
        //close modal after 1 seconds
        setTimeout(() => { modal.classList.toggle("show-modal"); }, 1000);
    }
}

/**
* Gets the input value from the text field
* returns x and y coordinate values
*/
function getInputValue() {
    let value = document.getElementById("text-input").value;
    let x = value.match('[a-jA-J]')
    let y = value.match('10|[1-9]');
    if (x == null || y == null) {
        alert("you have invalid coordinates");
        return null;
    }
    x = x[0];
    y = y[0].toLowerCase();
    //convert letter system to number coordinates
    x = x.charCodeAt(0) - 96;
    return [x, y];
}

/**
* Fires a missile at the target coordinates
* sets the cell color to red if a ship was hit
* sets the cell color to grey if missile missed
*/
function fire() {
    let coordinates = getInputValue();    
    let cell = document.getElementsByClassName('row-' + coordinates[1] + ' col-' + coordinates[0]);
    console.log(cell);
    cell = cell[0];
    console.log(cell);
    if (cell.classList.contains('fired')) {
        alert("you have already fired on this cell");
        return false;
    }
    if (cell.classList.contains('ship')) {
        cell.style.background = "red";
        cell.classList.remove('ship');
        cell.classList.add('sunk');
        toggleModal('hit', true);
        updateShipIndicators();
        checkWinCondition();
    } else {
        cell.style.background = "grey";
        toggleModal('miss', true);
    }
    cell.classList.add('fired')
}

/**
* Checks if the win conditions have been met
* */
function checkWinCondition() {
    let ships = document.getElementsByClassName('ship');
    if (ships.length == 0) {
        toggleModal('win', true);
    }
}

/**
* Checks if the ship is sunk
* param id: id of the ship
*/
function checkSunk(id) {
    let ships = document.getElementsByClassName('ship-type-' + id);

    for (let i = 0; i < ships.length; i++) {
        if (ships[i].classList.contains('sunk')) {
            continue;
        } else {
            return false;
        }
    }
    return true;
}

function updateShipIndicators() {
    let ships = document.getElementsByClassName("ship-indicator");

    for (let i = 0; i < ships.length; i++) {
        console.log();
        if (checkSunk(ships[i].id)) {
            ships[i].children[0].textContent = "SUNK";
            ships[i].children[0].classList.add("sunk");
        }
    }
}

function reload() {
    location.reload(false);
}

