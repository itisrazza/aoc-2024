import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.sql.Array;
import java.util.ArrayList;
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
        leftList.sort(Integer::compareTo);
        rightList.sort(Integer::compareTo);

        var distances = calculateDistances();
        System.out.printf("Total Distance: %d", distances.stream().reduce(0, Integer::sum));
    }

    private static void readLists(String filename) throws FileNotFoundException {
        var stream = new Scanner(new FileInputStream(filename));
        while (stream.hasNext()) {
            leftList.add(stream.nextInt());
            rightList.add(stream.nextInt());
        }
    }

    private static ArrayList<Integer> calculateDistances() {
        var distances = new ArrayList<Integer>(leftList.size());
        for (var i = 0; i < leftList.size(); i++) {
            distances.add(Math.abs( leftList.get(i) - rightList.get(i)));
        }

        return distances;
    }
}
