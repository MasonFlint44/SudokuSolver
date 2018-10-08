$(".cell").on("input", function(event) {
    trimText(event, 1);
});

$("#puzzle1").click(function() {
    getPuzzle(1);
});

$("#puzzle2").click(function () {
    getPuzzle(2);
});

$("#puzzle3").click(function () {
    getPuzzle(3);
});

$("#reset").click(resetPuzzle);

$("#solve").click(submitPuzzle);

function trimText(event, maxLength) {
    var element = event.target;

    // trim input to maxLength
    if ($(element).text().length >= maxLength) {
        $(element).text($(element).text().substr(0, maxLength));
    }

    // validate that input is integer > 0
    if ($.isNumeric($(element).text()) === false || $(element).text() === "0") {
        $(element).text("");
    }
}

function getPuzzle(index) {
    $.ajax({
        type: "GET",
        url: "/api/Sudoku/puzzle",
        data: { index: index },
        success: populateCells,
        dataType: "JSON"
    });
}

function populateCells(values) {
    resetPuzzle();

    var cells = $(".cell");
    values.forEach(function (item, index) {
        if(item === 0) { return; }
        $(cells[index]).text(item);
    });
}

function resetPuzzle() {
    $(".cell").each(function(index, value) {
        $(value).text("");
    });
    $("#executionTime").text("");
    $("#steps").html("");
}

function getCellValues() {
    var cells = new Array();
    $(".cell").each(function (index, value) {
        if ($(value).text() === "") {
            cells.push(0);
            return;
        }
        cells.push(parseInt($(value).text()));
    });
    return cells;
}

function submitPuzzle() {
    var cells = getCellValues();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Sudoku/solve",
        data: JSON.stringify(cells),
        success: processSolvedResponse,
        dataType: "JSON"
    }); 
}

function processSolvedResponse(data) {
    if (data.isValidPuzzle === false) {
        $("#executionTime").text(data.executionTime + " ms");
        $("#steps").html("Invalid puzzle");
        return;
    }

    if (data.solutionFound === false) {
        $("#executionTime").text(data.executionTime + " ms");
        $("#steps").html("Solution not found");
        return;
    }

    populateCells(data.solution);

    $("#executionTime").text(data.executionTime + " ms");

    var steps = "<br>";
    data.steps.forEach(function (step, index) {
        steps += printTab(1) + "Step " + (index + 1) + ": " + "<br>";
        steps += printTab(2) + "Degree: " + step.degree + "<br>";
        steps += printTab(2) + "Domain Size: " + step.domainSize + "<br>";
        steps += printTab(2) + "Selected Variable: { row: " + step.selectedRow + ", col: " + step.selectedCol + " }<br>";
    });
    $("#steps").html(steps);
}

function printTab(count) {
    var tab = "";
    for (var i = 0; i < count * 4; i++) {
        tab += "&nbsp";
    }
    return tab;
}
