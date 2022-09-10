// Silence some warnings that could distract from the exercise
#![allow(unused_variables, unused_mut, dead_code)]

enum Shot {
    Bullseye,
    Hit(f64),
    Miss,
}

impl Shot {
    fn points(&self) -> i32 {
        match self {
            Self::Bullseye => 5,
            Self::Hit(distance) if distance < &3.0 => 2,
            Self::Hit(distance) => 1,
            Self::Miss => 0,
        }
    }
}

fn main() {
    // Simulate shooting a bunch of arrows and gathering their coordinates on the target.
    let arrow_coords: Vec<Coord> = get_arrow_coords(5);

    let shots = take_shots(&arrow_coords);

    let mut total = 0;
    shots.iter().for_each(|shot| total += shot.points());

    println!("Final point total is: {}", total);
}

fn take_shots(arrow_coords: &Vec<Coord>) -> Vec<Shot> {
    let mut shots: Vec<Shot> = Vec::new();
    arrow_coords.iter().for_each(|coord| {
        println!();
        coord.print_description();
        let shot = if coord.distance_from_center() < 1.0 {
            println!("Bullseye! 🎯");
            Shot::Bullseye
        } else if coord.distance_from_center() < 5.0 {
            println!("Hit it! 💥");
            Shot::Hit(coord.distance_from_center())
        } else {
            println!("Not even close! 👎");
            Shot::Miss
        };
        shots.push(shot);
    });
    return shots;
}

// A coordinate of where an Arrow hit
#[derive(Debug)]
struct Coord {
    x: f64,
    y: f64,
}

impl Coord {
    fn distance_from_center(&self) -> f64 {
        (self.x.powf(2.0) + self.y.powf(2.0)).sqrt()
    }
    fn print_description(&self) {
        println!(
            "coord is {:.1} away, at ({:.1}, {:.1})",
            self.distance_from_center(),
            self.x,
            self.y
        );
    }
}

// Generate some random coordinates
fn get_arrow_coords(num: u32) -> Vec<Coord> {
    let mut coords: Vec<Coord> = Vec::new();
    for _ in 0..num {
        let coord = Coord {
            x: (rand::random::<f64>() - 0.5) * 12.0,
            y: (rand::random::<f64>() - 0.5) * 12.0,
        };
        coords.push(coord);
    }
    coords
}
