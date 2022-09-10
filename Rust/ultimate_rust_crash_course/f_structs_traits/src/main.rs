use std::io;

fn main() {
    let mut carrot = Carrot {
        percent_left: 100.0,
    };
    let mut grapes = Grapes { amount_left: 25 };

    println!();
    println!("Welcome to the mad bunny's playground, where I try to make silly things on rust and see how bad I fail");

    loop {
        println!();
        println!(
            "There are currently {:.02}% of the carrot and {} grapes left",
            carrot.percent_left, grapes.amount_left
        );
        let decided_action = decide_action();
        println!("The chosen action is {}", decided_action);
        if decided_action == "quit" {
            break;
        }

        let names_of_foods = vec![
            carrot.get_name_of_the_food(),
            grapes.get_name_of_the_food(),
        ];

        let decided_food = decide_food(names_of_foods);
        if decided_action == "eat" && decided_food == carrot.get_name_of_the_food() {
            carrot.bite();
        } if decided_action == "eat" && decided_food == grapes.get_name_of_the_food() {
            grapes.bite();
        } if decided_action == "nimble" && decided_food == carrot.get_name_of_the_food() {
            bunny_nibbles(&mut carrot);
        } if decided_action == "nimble" && decided_food == grapes.get_name_of_the_food() {
            bunny_nibbles(&mut grapes);
        } 

        if carrot.percent_left == 0.0 && grapes.amount_left == 0 {
            println!("The bunny ran out of food");
            break;
        }

    }
}

fn decide_action() -> String {
    loop {
        println!();
        println!("What would you like to do? (eat | nimble | quit)");
        let input = read_input();
        if input == "eat" || input == "nimble" || input == "quit" {
            return input;
        } else {
            println!("The input value is: {}, which wasn't recognized", input);
        }
    }
}

fn decide_food(names_of_foods: Vec<String>) -> String {
    loop {
        println!();
        println!("Which food ticks your fancy? Here's the menu:");
        for food in names_of_foods.iter() {
            println!("{}", food);
        }
        let input = read_input();
        if names_of_foods.contains(&input) {
            return names_of_foods.iter()
                .filter(|food| **food == input)
                .map(|food| (*food).to_string())
                .collect::<String>();
        }
        println!("Sorry, that's not on the menu 😞.");
        println!("Try again 😉");
    }
}

fn read_input() -> String {
    let mut input = String::new();
    io::stdin().read_line(&mut input).unwrap();
    return strip_trailing_newline(&input).to_string();
}

// https://stackoverflow.com/a/66401342/3016982
fn strip_trailing_newline(input: &str) -> &str {
    input
        .strip_suffix("\r\n")
        .or(input.strip_suffix("\n"))
        .unwrap_or(input)
}

fn bunny_nibbles<T: Food>(food: &mut T) {
    bunny_nibbles_x_times(food, 5);
}

fn bunny_nibbles_x_times<T: Food>(food: &mut T, amount_of_bites: i32) {
    let name_of_the_food = food.get_name_of_the_food();
    println!("Bunny starts nibbling {}", name_of_the_food);
    for _ in 0..amount_of_bites {
        food.bite();
    }
}

trait Food {
    fn bite(self: &mut Self);
    fn get_name_of_the_food(self: &Self) -> String {
        "Food".to_string()
    }
}

#[derive(Debug)] // This enables using the debugging format string "{:?}"
struct Carrot {
    percent_left: f32,
}

impl Food for Carrot {
    fn bite(self: &mut Self) {
        if self.percent_left <= 0.2 {
            self.percent_left = 0.0;
            return;
        }
        // Eat 20% of the remaining carrot. It may take awhile to eat it all...
        self.percent_left *= 0.8;
    }
    fn get_name_of_the_food(self: &Self) -> String {
        "Carrot".to_string()
    }
}

#[derive(Debug)]
struct Grapes {
    amount_left: i32,
}

impl Food for Grapes {
    fn bite(self: &mut Self) {
        self.amount_left -= 1;
    }
    fn get_name_of_the_food(self: &Self) -> String {
        "Grapes".to_string()
    }
}

fn _proposed_approach() {
    // Once you finish #1 above, this part should work.
    let mut carrot = Carrot {
        percent_left: 100.0,
    };
    carrot.bite();
    println!("I take a bite of the carrot: {:?}", carrot);
    carrot.bite();
    println!("I take a bite of the carrot: {:?}", carrot);

    // 4. Uncomment and adjust the code below to match how you defined your
    // Grapes struct.
    //
    let mut grapes = Grapes { amount_left: 25 };
    grapes.bite();
    println!("Eat a grape: {:?}", grapes);
    grapes.bite();
    println!("Eat a grape: {:?}", grapes);

    // Challenge: Uncomment the code below. Create a generic `bunny_nibbles`
    // function that:
    // - takes a mutable reference to any type that implements Bite
    // - calls `.bite()` several times
    // Hint: Define the generic type between the function name and open paren:
    //       fn function_name<T: Bite>(...)
    //
    bunny_nibbles(&mut carrot);
    println!("Bunny nibbles for awhile: {:?}", carrot);
    bunny_nibbles(&mut grapes);
    println!("Bunny nibbles for awhile: {:?}", grapes);
    bunny_nibbles_x_times(&mut grapes, 10);
    println!("Bunny nibbles for awhile: {:?}", grapes);
}
