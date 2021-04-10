

import { ShowPersonalCards } from './showCards.js';
import './deleteCards.js';
import './editCards.js';
import './sortCards.js';

function ready(func) {
	if (document.readyState != 'loading') {
		func();		
	} else {
		document.addEventListener('DOMContentLoaded', func);	
	}
}
ready(ShowPersonalCards);