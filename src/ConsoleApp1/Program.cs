using ConsoleApp1;

var list = new Lst<int>([1, 2, 3]);

var result = list.Select(x => x * 2)
                 .Select(x => x + 1);


