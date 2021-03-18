import Sortable from '/vendor/sortablejs/modular/sortable.complete.esm.js';

$(document).ready(function () {
	const listContainer = document.querySelector('.listofCards');

	var sortable = new Sortable(listContainer, {
		animation: 350,
		chosenClass: 'sortable-chosen',
		dragClass: 'sortable-drag',
		ghostClass: "sortable-ghost",
		forceFallback: true,
		fallbackClass: "dragged-item",
		dataIdAttr: 'data-id',
		filter: '.isNotDraggable',
		onSort: function () {
			var numOfCards = sortable.toArray();
			updatePositions(numOfCards);
		},
		onStart: function () {
			$('html').addClass("draggable-cursor");
		},
		onEnd: function () {
			$('html').removeClass("draggable-cursor");
		}
	});
})

function updatePositions(positions) {
	$.ajax({
		method: 'GET',
		url: 'https://localhost:44363/cards/updatecards',
		contentType: 'application/json',
		data: $.param({ data: positions }, true),
		error: function () {
			alert("Произошел сбой");
		}
	});
}