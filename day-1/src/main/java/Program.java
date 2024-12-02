import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;

public class Program {
    private final static ArrayList<Integer> leftList = new ArrayList<>();
    private final static ArrayList<Integer> rightList = new ArrayList<>();

    public static void main(String[] args) throws FileNotFoundException {
        String filename = "input";
        if (args.length >= 1) {
            filename = args[0];
        }

        readLists(filename);

        var distances = calculateDistances();
        System.out.printf("Total Distance: %d\n", distances.stream().reduce(0, Integer::sum));
        System.out.printf("Similarity Score: %d\n", calculateSimilarityScore());
    }

    private static void readLists(String filename) throws FileNotFoundException {
        var stream = new Scanner(new FileInputStream(filename));
        while (stream.hasNext()) {
            leftList.add(stream.nextInt());
            rightList.add(stream.nextInt());
        }
    }

    private static ArrayList<Integer> calculateDistances() {
        var leftSorted = leftList.stream().sorted().toList();
        var rightSorted = rightList.stream().sorted().toList();

        var distances = new ArrayList<Integer>(leftSorted.size());
        for (var i = 0; i < leftSorted.size(); i++) {
            distances.add(Math.abs( leftSorted.get(i) - rightSorted.get(i)));
        }

        return distances;
    }

    private static Integer calculateSimilarityScore() {
        var map = new HashMap<Integer, Integer>();
        for (var num : rightList) {
            map.put(num, map.getOrDefault(num, 0) + 1);
        }

        var score = 0;
        for (var num : leftList) {
            score += num * map.getOrDefault(num, 0);
        }

        return score;
    }
}
