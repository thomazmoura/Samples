fn main() {
    println!("This is how you can print a simple sentence!");

    println!("\nThis is how you can print a simple string value (modern language version):");
    let first_string = "Rust World";
    println!("Hello {first_string}! From unmutable string.");

    println!("\nThis how you make a mutable value:");
    let mut mutable_string = "First value";
    println!("This is the value of mutable_string before mutation: {mutable_string}");
    mutable_string = "Next value!";
    println!("This is the value of mutable_string after mutation: {mutable_string}");

    println!("\nMutation is not as cool in programming it in X-Men, so use it with care.");

    println!("\nThis how you can declare and set multiple variables at once (because why not?):");
    let (first_value, second_value) = ("Answer to the Ultimate Question of Life, the Universe, and Everything", 42);
    println!("\n The ({first_value}) is ({second_value}).");

    println!("\nYou can also format values like that:");
    let pi_value = 3.14159265359;
    println!("\nThis a compact way to format pi: {pi_value:.3}");

    println!("\nYou can also make it constant if you're one of those:");
    const CONST_PI_VALUE:f64 = 3.14159265359;
    println!("\nThis a quite a weird way to format pi: {CONST_PI_VALUE:05.2}");
}

