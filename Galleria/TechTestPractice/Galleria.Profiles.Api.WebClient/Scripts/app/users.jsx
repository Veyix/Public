/// <reference path="errorHandler.jsx" />
/// <reference path="api.js" />

class User extends React.Component {
    constructor(props) {
        super(props);

        this.viewUser = this.viewUser.bind(this);
        this.editUser = this.editUser.bind(this);
        this.deleteUser = this.deleteUser.bind(this);
    }

    viewUser(event) {
        event.preventDefault();

        if (this.props.onViewUser) {
            this.props.onViewUser(this.props.user.userId);
        }
    }

    editUser(event) {
        event.preventDefault();

        if (this.props.onEditUser) {
            this.props.onEditUser(this.props.user.userId);
        }
    }

    deleteUser(event) {
        event.preventDefault();

        if (this.props.onDeleteUser) {
            this.props.onDeleteUser(this.props.user.userId);
        }
    }

    render() {
        return (
            <div className="row user">
                <div className="col-xs-8 user-info">
                    {this.props.user.title} {this.props.user.forename} {this.props.user.surname}
                </div>
                <div className="col-xs-4 actions">
                    <a onClick={this.viewUser}>view</a>
                    <a onClick={this.editUser}>edit</a>
                    <a onClick={this.deleteUser}>delete</a>
                </div>
            </div>
        );
    }
}

class UsersView extends React.Component {
    constructor(props) {
        super(props);

        this.errorHandler = props.errorHandler;
        this.api = props.api;

        this.state = {
            users: []
        };
    }

    componentWillMount() {
        this.refresh();
    }

    refresh() {

        // Use the API to get the users
        this.api.getUsers(
            (response) => this.setState({ users: response })
        );
    }

    render() {
        const users = this.state.users.map(
            (user) => <User key={user.userId} user={user} />
        );

        return (
            <div className="panel panel-default">
                <div className="panel-heading">
                    <h3>Users</h3>
                </div>

                <div className="panel-body">
                    {users}
                </div>
            </div>
        );
    }
}