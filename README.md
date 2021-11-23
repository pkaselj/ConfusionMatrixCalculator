# ConfusionMatrixCalculator

An application that takes confusion matrix in *.csv* format and calculates:

- Recall
- Specificity
- Accuracy
- F1 Measure

for each class and a weighed average of those measures.

## Input Format
All entries are separated by a comma (,) and lines are separated by the new line character (\n).

First row is a list of all classes. Classes in columns represent *EVALUATED CLASS* of a data unit from dataset
while classes in rows (which are not written but implicitly follow order from top to bottom as classes in the first row
follow from left to right) represent *TRUE CLASS* of a given data unit from dataset.

For example, confusion matrix represented by:

![Confusion Matrix](/ConfusionMatrixCalculator/Assets/confusionMatrix.PNG)
  
 Would be written (in raw *.csv* file) as follows:
 
![RAW Confusion Matrix](/ConfusionMatrixCalculator/Assets/rawConfusionMatrix.PNG)
 
 ## Output Format
 
 The application outputs the confusion matrix, and evaluation measures in console.
 
 The following is the output for the confusion matrix given above:
 
 ![Output Format](/ConfusionMatrixCalculator/Assets/result.png)

# Author
name: Petar Kaselj
e-mail: petar.kaselj.00@gmail.com
