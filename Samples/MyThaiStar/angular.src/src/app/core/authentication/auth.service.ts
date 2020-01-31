import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { find } from 'lodash';
import { config } from '../../config';

@Injectable()
export class AuthService {
    private logged: boolean = false;
    private user: string = '';
    private currentRole: string = 'CUSTOMER';
    private token: string;

    public isLogged(): boolean {
        return this.logged;
    }

    public setLogged(login: boolean): void {
        this.logged = login;
    }

    public getUser(): string {
        return this.user;
    }

    public setUser(username: string): void {
        this.user = username;
    }

    public getToken(): string {
        return this.token;
    }

    public setToken(token: string): void {
        this.token = token;
    }

    public setRole(role: string): void {
        this.currentRole = role;
    }

    public getPermission(roleName: string): number {
        return find(config.roles, {name: roleName}).permission;
    }

    public isPermited(userRole: string): boolean {
        return this.getPermission(this.currentRole) === this.getPermission(userRole);
    }
}
