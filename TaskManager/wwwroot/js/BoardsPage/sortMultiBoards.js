import Sortable from '/vendor/sortablejs/modular/sortable.complete.esm.js';

$(document).ready(function () {
	const boardsContainer = document.querySelector('.allMultiBoards');

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
			var boardsPositionsPattern = boardsPositions.join("&ids=");
			SaveMultiBoardsPositions(boardsPositionsPattern);
		},
		onStart: function () {
			$('html').addClass("draggable-cursor");
		},
		onEnd: function () {
			$('html').removeClass("draggable-cursor");
		}
	});
});

function SaveMultiBoardsPositions(positions) {
	$.ajax({
		method: 'PUT',
		url: 'https://localhost:44363/boards/updatemultiboards?' + $.param({ ids: positions }),
		contentType: 'application/json',
		error: function () {
			alert("Произошел сбой");
		}
	});
}