# Silly example of date comparison to get age

from datetime import date

birth_year = int(input("Year of birth: "))
birth_month = int(input("Month of birth: "))
birth_day = int(input("Day of birth: "))
birthday_date = date(birth_year, birth_month, birth_day)
age = date.today() - birthday_date

result = "You are " + str(int(age.days / 365.25)) + " years old"
print(result)

