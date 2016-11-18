/// <reference path="errorHandler.jsx" />
/// <reference path="api.js" />

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
            (user) => (
                <div className="row">
                    <div className="col-xs-12">
                        {user.title} {user.forename} {user.surname}
                    </div>
                </div>
            )
        );

        return (
            <div>
                <h3>Users</h3>

                {users}
            </div>
        );
    }
}