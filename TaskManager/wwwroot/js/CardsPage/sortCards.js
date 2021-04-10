

import Sortable from '/vendor/sortablejs/modular/sortable.complete.esm.js';

const listContainer = document.querySelector('.listofCards');
const sortable = new Sortable(listContainer, {
	animation: 350,
	chosenClass: 'sortable-chosen',
	dragClass: 'sortable-drag',
	ghostClass: "sortable-ghost",
	forceFallback: true,
	fallbackClass: "dragged-item",
	dataIdAttr: 'data-id',
	filter: '.isNotDraggable',
	onSort: function () {
		var sequenceOfIds = sortable.toArray();
		UpdatePositions(sequenceOfIds);
	},
	onStart: function () {
		$('html').addClass("draggable-cursor");
	},
	onEnd: function () {
		$('html').removeClass("draggable-cursor");
	}
});
function UpdatePositions(sequenceOfIds) {
	var request = new XMLHttpRequest();
	let url = new URL('https://localhost:44363/cards/updatecards');
	url.searchParams.set('ids', JSON.stringify(sequenceOfIds));
	request.open('GET', url, true);
	request.setRequestHeader('Content-Type', 'application/json');
	request.onload = function () {
		if (!this.status >= 200 && !this.status < 400) {
			alert("Error");
		}
	}
	request.send(null);
}