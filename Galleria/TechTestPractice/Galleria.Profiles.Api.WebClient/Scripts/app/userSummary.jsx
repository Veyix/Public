class UserSummary extends React.Component {
    constructor(props) {
        super(props);

        this.viewUser = this.viewUser.bind(this);
        this.editUser = this.editUser.bind(this);
        this.deleteUser = this.deleteUser.bind(this);
    }

    viewUser(event) {
        event.preventDefault();

        if (this.props.onViewUser) {
            this.props.onViewUser(this.props.user.id);
        }
    }

    editUser(event) {
        event.preventDefault();

        if (this.props.onEditUser) {
            this.props.onEditUser(this.props.user.id);
        }
    }

    deleteUser(event) {
        event.preventDefault();

        if (this.props.onDeleteUser) {
            this.props.onDeleteUser(this.props.user.id);
        }
    }

    render() {
        return (
            <div className="row user">
                <div className="col-xs-10 user-info">
                    {this.props.user.title} {this.props.user.forename} {this.props.user.surname}
                </div>
                <div className="col-xs-2 text-right actions">
                    <a onClick={this.viewUser}>view</a>
                    <a onClick={this.editUser}>edit</a>
                    <a onClick={this.deleteUser}>delete</a>
                </div>
            </div>
        );
    }
}