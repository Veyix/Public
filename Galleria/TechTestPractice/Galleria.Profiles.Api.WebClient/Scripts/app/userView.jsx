class UserReadonlyView extends React.Component {
    render() {
        const user = this.props.user;

        return (
            <div className="text-left">
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>User Id</strong></div>
                        <div className="text-display">{user.id}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Company Id</strong></div>
                        <div className="text-display">{user.companyId}</div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>Title</strong></div>
                        <div className="text-display">{user.title}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Forename</strong></div>
                        <div className="text-display">{user.forename}</div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>Surname</strong></div>
                        <div className="text-display">{user.surname}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Date of Birth</strong></div>
                        <div className="text-display">{user.dateOfBirth}</div>
                    </div>
                </div>
                <div className="row text-muted">
                    <div className="col-sm-6">
                        <div><strong>Created</strong></div>
                        <div className="text-display">{user.createdDate}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Last Changed</strong></div>
                        <div className="text-display">{user.lastChangedDate}</div>
                    </div>
                </div>
            </div>
        );
    }
}

class UserEditView extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            user: this.props.user
        };

        this.handleTitleChanged = this.handleTitleChanged.bind(this);
        this.handleForenameChanged = this.handleForenameChanged.bind(this);
        this.handleSurnameChanged = this.handleSurnameChanged.bind(this);
        this.handleDateOfBirthChanged = this.handleDateOfBirthChanged.bind(this);
    }

    handleTitleChanged(event) {
        
        var user = this.state.user;
        user.title = event.target.value;

        this.setState({ user: user });
    }
    
    handleForenameChanged(event) {

        var user = this.state.user;
        user.forename = event.target.value;

        this.setState({ user: user });
    }

    handleSurnameChanged(event) {

        var user = this.state.user;
        user.surname = event.target.value;

        this.setState({ user: user });
    }

    handleDateOfBirthChanged(event) {

        var user = this.state.user;
        user.dateOfBirth = event.target.value;

        this.setState({ user: user });
    }

    render() {
        const user = this.state.user;

        return (
            <div className="text-left">
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>User Id</strong></div>
                        <div className="text-display">{user.id}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Company Id</strong></div>
                        <div className="text-display">{user.companyId}</div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>Title</strong></div>
                        <input type="text" value={user.title} onChange={this.handleTitleChanged} />
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Forename</strong></div>
                        <input type="text" value={user.forename} onChange={this.handleForenameChanged} />
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>Surname</strong></div>
                        <input type="text" value={user.surname} onChange={this.handleSurnameChanged} />
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Date of Birth</strong></div>
                        <input type="date" value={user.dateOfBirth} onChange={this.handleDateOfBirthChanged} />
                    </div>
                </div>
                <div className="row text-muted">
                    <div className="col-sm-6">
                        <div><strong>Created</strong></div>
                        <div className="text-display">{user.createdDate}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Last Changed</strong></div>
                        <div className="text-display">{user.lastChangedDate}</div>
                    </div>
                </div>
            </div>
        );
    }
}

class User extends React.Component {
    render() {
        const user = this.props.user;

        if (this.props.isEditMode) {
            return <UserEditView user={user} onSave={this.save} />;
        }
        else {
            return <UserDisplayView user={user} />
        }
    }
}