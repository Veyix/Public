class LoginForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            username: '',
            password: ''
        };

        this.onUsernameChanged = this.onUsernameChanged.bind(this);
        this.onPasswordChanged = this.onPasswordChanged.bind(this);
        this.submit = this.submit.bind(this);
    }

    onUsernameChanged(event) {
        this.setState({ username: event.target.value });
    }

    onPasswordChanged(event) {
        this.setState({ password: event.target.value });
    }

    submit(event) {
        event.preventDefault();

        if (this.props.submit) {
            this.props.submit(this.state.username, this.state.password);
        }
    }

    render() {
        return (
            <form className="login-form">
                <h3>Login</h3>

                <label for="username">Username</label>
                <input id="username" type="text" className="form-control" onChange={this.onUsernameChanged} />

                <label for="password">Password</label>
                <input id="password" type="password" className="form-control" onChange={this.onPasswordChanged} />

                <input type="submit" className="btn btn-default" value="Submit" onClick={this.submit} />
            </form>
        );
    }
}