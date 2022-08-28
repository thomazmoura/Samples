const STARTING_MISSILES: i32 = 8;
const READY_AMOUNT: i32 = 2;

fn main() {
    let (mut missiles, ready):(i32, i32) = (STARTING_MISSILES, READY_AMOUNT);
    println!("Firing {ready} of my {missiles} missiles...");
    missiles -= ready;
    println!("Only {missiles} missiles left.");
}
