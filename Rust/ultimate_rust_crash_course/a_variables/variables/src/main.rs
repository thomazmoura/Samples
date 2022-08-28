const STARTING_MISSILES: i32 = 8;
const READY_AMOUNT: i32 = 2;

fn main() {
    let mut remaining_missiles = STARTING_MISSILES;
    for i in 0..5 {
        println!("Running for the {i:?}th time");
        remaining_missiles = fire_missiles(remaining_missiles);
    }
}

fn fire_missiles(current_missiles: i32) -> i32 {
    if current_missiles <= 0 {
        println!("No missiles left to fire!");
        return 0;
    }

    let (mut missiles, ready):(i32, i32) = (current_missiles, READY_AMOUNT);
    println!("Firing {ready} of my {missiles} missiles...");
    missiles = calculate_remaining_missiles(missiles, ready);
    println!("Only {missiles} missiles left.");
    return missiles;
}

fn calculate_remaining_missiles(current_missiles: i32, ready_amount: i32) -> i32 { current_missiles - ready_amount }
