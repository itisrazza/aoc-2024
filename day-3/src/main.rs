use std::fs::read_to_string;
use std::iter;

use regex::{CaptureMatches, Regex};

#[derive(Debug)]
enum Command {
    Do,
    Dont,
    Mul(u32, u32),
}

fn main() {
    let filename = match std::env::args().nth(1) {
        Some(arg) => arg,
        None => "input.txt".into(),
    };
    println!("Read file from {}", &filename);
    let input = read_to_string(filename).unwrap();

    let do_pattern = Regex::new(r"do\(\)").unwrap();
    let dont_pattern = Regex::new(r"don't\(\)").unwrap();
    let mull_pattern = Regex::new(r"mul\((\d+),(\d+)\)").unwrap();

    let mut all_captures: Vec<_> = mull_pattern
        .captures_iter(&input)
        .chain(do_pattern.captures_iter(&input))
        .chain(dont_pattern.captures_iter(&input))
        .collect();
    all_captures.sort_by(|a, b| a.get(0).unwrap().start().cmp(&b.get(0).unwrap().start()));

    let mut part_1_total = 0;
    let mut part_2_total = 0;
    let mut do_active = true;
    for command in all_captures.iter().map(|capture| {
        if capture.get(0).unwrap().as_str().eq("do()") {
            Command::Do
        } else if capture.get(0).unwrap().as_str().eq("don't()") {
            Command::Dont
        } else {
            let lhs: u32 = capture.get(1).unwrap().as_str().parse().unwrap();
            let rhs: u32 = capture.get(2).unwrap().as_str().parse().unwrap();
            Command::Mul(lhs, rhs)
        }
    }) {
        println!("{:?}", &command);
        match command {
            Command::Do => do_active = true,
            Command::Dont => do_active = false,
            Command::Mul(a, b) => {
                part_1_total = part_1_total + a * b;
                if do_active {
                    part_2_total = part_2_total + a * b;
                }
            }
        }
    }

    println!("Part 1 Total: {}", part_1_total);
    println!("Part 2 Total: {}", part_2_total);
}
