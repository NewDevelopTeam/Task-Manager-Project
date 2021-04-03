import Sortable from '/vendor/sortablejs/modular/sortable.complete.esm.js';

$(document).ready(function () {
    const boardsContainer = document.querySelector('.allPersonalBoards');

	var sortable = new Sortable(boardsContainer, {
		animation: 350,
		chosenClass: 'sortable-chosen',
		dragClass: 'sortable-drag',
		ghostClass: "sortable-ghost",
		forceFallback: true,
		fallbackClass: "dragged-item",
		dataIdAttr: 'data-id',
		filter: '.isNotDraggable',
		onSort: function () {
			var boardsPositions = sortable.toArray();
			SavePersonalBoardsPositions(boardsPositions)
		},
		onStart: function () {
			$('html').addClass("draggable-cursor");
		},
		onEnd: function () {
			$('html').removeClass("draggable-cursor");
		}
	});
});

function SavePersonalBoardsPositions(positions) {
	$.ajax({
		method: 'GET',
		url: 'https://localhost:44363/boards/updatepersonalboards',
		contentType: 'application/json',
		data: $.param({ data: positions }, true),
		error: function () {
			alert("Произошел сбой");
        }
	});
}

