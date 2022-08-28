// Silence some warnings so they don't distract from the exercise.
#![allow(unused_variables)]
use rand::*;

fn main() {
    let mut random_handler = thread_rng();
    let width = random_handler.gen_range(0..100);
    let height = random_handler.gen_range(0..50);
    let depth = random_handler.gen_range(0..10);

    println!("The input for this function is: (width={width}, height={height}, depth={depth})");
    // 1. Try running this code with `cargo run` and take a look at the error.
    //
    // See if you can fix the error. It is right around here, somewhere.  If you succeed, then
    // doing `cargo run` should succeed and print something out.
    {
        let area = area_of(width, height);
        println!("Area is {}", area);
    }

    // 2. The area that was calculated is not correct! Go fix the area_of() function below, then run
    //    the code again and make sure it worked (you should get an area of 28).

    // 3. Uncomment the line below.  It doesn't work yet because the `volume` function doesn't exist.
    //    Create the `volume` function!  It should:
    //    - Take three arguments of type i32
    //    - Multiply the three arguments together
    //    - Return the result (which should be 280 when you run the program).
    //
    // If you get stuck, remember that this is *very* similar to what `area_of` does.
    //
    println!("Volume is {}", volume_of(width, height, depth));
}

fn area_of(x: i32, y: i32) -> i32 {
    // 2a. Fix this function to correctly compute the area of a rectangle given
    // dimensions x and y by multiplying x and y and returning the result.
    //
    x * y
    // Challenge: It isn't idiomatic (the normal way a Rust programmer would do things) to use
    //            `return` on the last line of a function. Change the last line to be a
    //            "tail expression" that returns a value without using `return`.
    //            Hint: `cargo clippy` will warn you about this exact thing.
}

fn volume_of(width: i32, height: i32, depth: i32) -> i32 {
    width * height * depth
}
