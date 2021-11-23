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

  a   b   c   d   <--- classified as
349  17   1   0 |   a
 28  90   2   0 |   b
  0  11   5   1 |   c
  0   8   0   6 |   d
  
 Would be written (in raw *.csv* file) as follows:
 
 a,b,c,d
 349,17,1,0
 28,90,2,0
 0,11,5,1
 0,8,0,6
 
 ## Output Format
 
 The application outputs the confusion matrix, and evaluation measures in console.
 
 The following is the output for the confusion matrix given above:
 
 === Evaluated classes ===
        a       b       c       d
a       349     17      1       0
b       28      90      2       0
c       0       11      5       1
d       0       8       0       6
====================STATISTICS===================
Class   Count   Recall  Spec    Accur   F1
a       367     95.10   78.29   92.57   93.82
b       120     75.00   90.91   71.43   73.17
c       17      29.41   99.33   62.50   40.00
d       14      42.86   99.78   85.71   57.14
-------------------------------------------------
AVG     518     86.87   82.49   86.50   86.28
=================================================
 
 
