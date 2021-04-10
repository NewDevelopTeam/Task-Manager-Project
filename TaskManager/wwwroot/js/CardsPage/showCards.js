

export function ShowPersonalCards() {
	const elem = document.getElementById("transCards");
	const jsonCards = elem.getAttribute("data-cardsAll");
	const arrofCards = JSON.parse(jsonCards);
	const listContainer = document.querySelector('.listofCards');
	const fragment = document.createDocumentFragment();

	listContainer.innerHTML = '';
	const objOfCards = arrofCards.reduce((acc, item) => {
		acc[item.RowNo] = item;
		return acc;
	}, {});

	if (Object.keys(objOfCards).length == 0) {
		const li = EmptyItemTemplate();
		fragment.appendChild(li);
	} else {
		Object.values(objOfCards).forEach(task => {
			const li = ListItemTemplate(task);
			fragment.appendChild(li);
		})
	}
	listContainer.appendChild(fragment);	
};

function ListItemTemplate({ Id, CardDescription } = {}) {

	const li = document.createElement('li');
	li.classList.add("card-personal", "p-2", "card-background", "border", "border-secondary", "rounded");
	li.setAttribute("data-id", Id);

	const cardWrapper = document.createElement('div');
	cardWrapper.classList.add("card-wrapper", "d-flex", "flex-nowrap", "mb-4");

	const cardHeader = document.createElement('div');
	cardHeader.classList.add("card-description", "mr-1", "flex-grow-1");
	cardHeader.textContent = CardDescription;

	const cardPanel = document.createElement('div');
	cardPanel.classList.add("card-panel", "d-flex", "flex-column", "flex-wrap", "align-selt-start");

	const cardDelete = document.createElement('div');
	cardDelete.classList.add("card-delete", "card-button");
	cardDelete.setAttribute("data-toggle", "modal");
	cardDelete.setAttribute("data-target", "#delete-card");
	cardDelete.id = Id;

	const cardEdit = document.createElement('div');
	cardEdit.classList.add("card-edit", "card-button");
	cardEdit.setAttribute("data-toggle", "modal");
	cardEdit.setAttribute("data-target", "#edit-card");
	cardEdit.id = Id;

	cardPanel.appendChild(cardDelete);
	cardPanel.appendChild(cardEdit);

	cardWrapper.appendChild(cardHeader);
	cardWrapper.appendChild(cardPanel);

	li.appendChild(cardWrapper);

	return li;
}
export function EmptyItemTemplate() {
	const li = document.createElement('li');
	li.classList.add("card", "isNotDraggable");

	const cardHeader = document.createElement('div');
	cardHeader.classList.add("card-header", "py-4");
	cardHeader.textContent = "У Вас отсутствуют персональные карточки";
	li.appendChild(cardHeader);

	return li;
};

