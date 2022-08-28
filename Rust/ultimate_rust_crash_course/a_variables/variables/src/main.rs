const STARTING_MISSILES: i32 = 8;
const READY_AMOUNT: i32 = 2;

fn main() {
    fire_missiles();
}

fn fire_missiles() {
    let (mut missiles, ready):(i32, i32) = (STARTING_MISSILES, READY_AMOUNT);
    println!("Firing {ready} of my {missiles} missiles...");
    missiles = calculate_remaining_missiles(missiles);
    println!("Only {missiles} missiles left.");
}

fn calculate_remaining_missiles(current_missiles: i32) -> i32 { current_missiles - READY_AMOUNT }
