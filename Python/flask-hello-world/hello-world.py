from flask import Flask

flask = Flask(__name__)
@flask.route("/")
def root():
    return '{ message: "Hello World, I''m Flask "}'

flask.run()
