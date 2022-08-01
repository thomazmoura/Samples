Write-Output "Insert a number for the day of the week (1~7 with 1 being sunday and 7 being saturday)"

$InputValue = Read-Host 
$InputNumber = $InputValue -as [int]
if( !($InputNumber) ) {
  Write-Error "The input value is not a valid number" -ErrorAction Stop
}

$DayOfTheWeek = switch ($InputValue % 7)
{
  1 { 'Sunday' }
  2 { 'Monday' }
  3 { 'Tuesday' }
  4 { 'Wednesday' }
  5 { 'Thursday' }
  6 { 'Friday' }
  default { 'Saturday' }
}

$NumberOfWeeks = $InputValue/7 -as [int]
if($NumberOfWeeks -le 1) {
  Write-Output "The chosen day was: $DayOfTheWeek"
} else {
  Write-Output "The chosen day was: $DayOfTheWeek (on the ${NumberOfWeeks}th week)"
}
