// i - rzad zerujacy
// j - rzad zerowany

// wartosci konczace petle powinny byc inne ale to macierz kwadratowa

 for (int i=0; i<rowCount; i++){

	for (int j=i+1; j<rowCount; j++){

		double multiplier = data[j][i]/data[i][i];

		for (col = i; col<colCount; col++){

			data[j][col] -= data[i][col] * multiplier

		}


	}

}

