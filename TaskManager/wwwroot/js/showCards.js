const elem = document.getElementById("transCards");
const value = elem.getAttribute("data-cardsAll");
var arr_from_json = JSON.parse(value);

(function (listofCards) {

	const listContainer = document.querySelector('.listofCards');

    const objOfCards = listofCards.reduce((acc, obj) => {
		acc[obj.Id] = obj;
		console.log(obj.Id);
		return acc
	}, {});

	console.log(objOfCards);

	renderAllCards(objOfCards);

    function renderAllCards(objOfCards) {
		const fragment = document.createDocumentFragment();

		if (Object.keys(objOfCards).length == 0) {
			const li = EmptyItemTemplate();
			fragment.appendChild(li);
		} else {
			Object.values(objOfCards).forEach(task => {
				const li = ListItemTemplate(task);
				fragment.appendChild(li);
			})
        }

		function ListItemTemplate({ Id, CardDescription} = {}) {
			const li = document.createElement('li');
			li.classList.add("card");

			const cardHeader = document.createElement('div');
			cardHeader.classList.add("card-header");
			cardHeader.textContent = CardDescription;

			const cardPanel = document.createElement('div');
			cardPanel.classList.add("card-panel", "d-flex", "flex-wrap", "m-2", "flex-row-reverse");

			const cardEdit = document.createElement('button');
			cardEdit.classList.add("card-edit", "btn", "btn-sm", "btn-dark", "m-1");
			cardEdit.setAttribute("data-toggle", "modal");
			cardEdit.setAttribute("data-target", "#edit-card");
			cardEdit.textContent = "Редактировать";
			cardEdit.id = Id;

			const cardDelete = document.createElement('button');
			cardDelete.classList.add("card-delete", "btn", "btn-sm", "btn-danger", "m-1");
			cardDelete.setAttribute("data-toggle", "modal");
			cardDelete.setAttribute("data-target", "#delete-card");
			cardDelete.textContent = "Удалить";
			cardDelete.id = Id;

			cardPanel.appendChild(cardDelete);
			cardPanel.appendChild(cardEdit);

			li.appendChild(cardHeader);
			li.appendChild(cardPanel);

			return li;
		}
		function EmptyItemTemplate() {
			const li = document.createElement('li');
			li.classList.add("card");

			const cardHeader = document.createElement('div');
			cardHeader.classList.add("card-header", "py-4");
			cardHeader.textContent = "У Вас отсутствуют персональные карточки";
			li.appendChild(cardHeader);

			return li;
		}

		listContainer.appendChild(fragment);
    }
})(arr_from_json);