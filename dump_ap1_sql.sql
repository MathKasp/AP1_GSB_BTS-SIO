-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : lun. 28 avr. 2025 à 08:53
-- Version du serveur : 5.7.24
-- Version de PHP : 8.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `ap1_sql`
--

-- --------------------------------------------------------

--
-- Structure de la table `etat`
--

CREATE TABLE `etat` (
  `id_etat` int(11) NOT NULL,
  `etat` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `etat`
--

INSERT INTO `etat` (`id_etat`, `etat`) VALUES
(1, 'EN ATTENTE'),
(2, 'EN COURS'),
(3, 'VALIDER'),
(4, 'REFUSER');

-- --------------------------------------------------------

--
-- Structure de la table `fiche_de_frais`
--

CREATE TABLE `fiche_de_frais` (
  `id_fiche_frais` int(11) NOT NULL,
  `id_utilisateur` int(11) NOT NULL,
  `date_creation` date NOT NULL,
  `date_fin` date NOT NULL,
  `id_etat` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `fiche_de_frais`
--

INSERT INTO `fiche_de_frais` (`id_fiche_frais`, `id_utilisateur`, `date_creation`, `date_fin`, `id_etat`) VALUES
(1, 1, '2024-05-11', '2024-06-10', 1),
(2, 1, '2024-06-11', '2024-07-10', 1),
(3, 3, '2024-05-11', '2024-06-10', 4),
(4, 3, '2024-06-11', '2024-07-10', 1),
(5, 3, '2025-04-11', '2025-05-10', 2);

-- --------------------------------------------------------

--
-- Structure de la table `frais_forfait`
--

CREATE TABLE `frais_forfait` (
  `id_forfait` int(11) NOT NULL,
  `date_frais` date NOT NULL,
  `id_fiche_frais` int(11) NOT NULL,
  `id_type` int(11) NOT NULL,
  `Motif` varchar(500) NOT NULL,
  `Valeur` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `frais_forfait`
--

INSERT INTO `frais_forfait` (`id_forfait`, `date_frais`, `id_fiche_frais`, `id_type`, `Motif`, `Valeur`) VALUES
(34, '2024-06-06', 1, 2, 'Repas pro a marseille', '20.00'),
(35, '2024-05-27', 1, 1, 'Nuitée chez un tier', '80.00'),
(36, '2024-05-31', 1, 3, 'transport en commun de l\'équipe', '16.63'),
(38, '2024-06-06', 1, 3, 'Mcdoo', '2.68'),
(43, '2024-06-14', 2, 1, 'Nuit Hotel part dieu', '80.00'),
(44, '2024-06-14', 2, 3, 'Déplacement pro part dieu', '16.75'),
(45, '2024-06-14', 2, 2, 'Repas professionnel', '20.00'),
(46, '2024-06-14', 2, 2, 'Cantine', '20.00'),
(47, '2024-06-18', 3, 2, 'Repas cantine', '20.00'),
(48, '2024-06-11', 4, 1, 'hotel', '80.00'),
(49, '2024-06-11', 4, 2, 'repas au restaurant', '20.00'),
(50, '2024-06-12', 4, 3, 'covoiturage paris', '39.30'),
(51, '2024-06-18', 2, 3, 'bonjour', '9.38'),
(52, '2025-04-12', 5, 1, 'Sommeil chez un client.', '80.00');

-- --------------------------------------------------------

--
-- Structure de la table `frais_hors_forfait`
--

CREATE TABLE `frais_hors_forfait` (
  `id_Hforfait` int(11) NOT NULL,
  `nom` varchar(500) NOT NULL,
  `valeur` decimal(10,2) NOT NULL,
  `date_frais` date NOT NULL,
  `id_fiche_frais` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `frais_hors_forfait`
--

INSERT INTO `frais_hors_forfait` (`id_Hforfait`, `nom`, `valeur`, `date_frais`, `id_fiche_frais`) VALUES
(1, 'Achat de Bottes', '50.00', '2024-05-16', 1),
(2, 'Nourrtiture pour la TEAm', '36.37', '2024-06-04', 1),
(4, 'test double', '0.48', '2024-05-27', 1),
(5, 'Expetion expetionnelement exeptionnel', '15.00', '2024-06-02', 1),
(10, 'Amende a cause du véhicule pro', '80.00', '2024-06-18', 2),
(11, 'J\'allais pas trop vite promis', '90.00', '2024-06-18', 2),
(12, 'Je suis au poste de police faut payer ma caution rip rip rip', '200.00', '2024-06-19', 2),
(13, 'Achat de pc pro', '600.00', '2024-06-27', 2),
(14, 'Hotel achat savon', '15.99', '2024-06-19', 3),
(15, 'Achat d\'un pull proffeszionnel a ventilateu rporporpor', '65.00', '2024-06-27', 4),
(16, 'entrée de tournois.', '25.00', '2024-06-12', 4),
(17, 'Frais port de départ', '36.00', '2024-06-21', 4),
(18, 'Chaussettes de velours', '12.60', '2024-06-16', 4),
(19, 'Enième test', '56.20', '2024-06-22', 4),
(20, 'Achat d\'un ordi pro', '400.00', '2024-06-23', 2),
(21, 'Particulier', '60.00', '2025-04-12', 5);

-- --------------------------------------------------------

--
-- Structure de la table `role`
--

CREATE TABLE `role` (
  `id_role` int(11) NOT NULL,
  `role` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `role`
--

INSERT INTO `role` (`id_role`, `role`) VALUES
(1, 'Visiteur'),
(2, 'Comptable'),
(3, 'Administrateur');

-- --------------------------------------------------------

--
-- Structure de la table `type_frais`
--

CREATE TABLE `type_frais` (
  `id_type` int(11) NOT NULL,
  `nom` varchar(50) NOT NULL,
  `valeur` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `type_frais`
--

INSERT INTO `type_frais` (`id_type`, `nom`, `valeur`) VALUES
(1, 'Nuitée', '80.00'),
(2, 'Repas', '20.00'),
(3, 'frais kilometrique', '0.67');

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

CREATE TABLE `utilisateur` (
  `id_utilisateur` int(11) NOT NULL,
  `nom` varchar(50) NOT NULL,
  `mdp` varchar(50) NOT NULL,
  `id_role` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `utilisateur`
--

INSERT INTO `utilisateur` (`id_utilisateur`, `nom`, `mdp`, `id_role`) VALUES
(1, 'BARTH Antonin', 'salant', 1),
(2, 'CHOC Anais', 'coco', 2),
(3, 'HAM Theo', 'admin', 1);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `etat`
--
ALTER TABLE `etat`
  ADD PRIMARY KEY (`id_etat`);

--
-- Index pour la table `fiche_de_frais`
--
ALTER TABLE `fiche_de_frais`
  ADD PRIMARY KEY (`id_fiche_frais`),
  ADD KEY `id_etat` (`id_etat`),
  ADD KEY `id_utilisateur_fk` (`id_utilisateur`);

--
-- Index pour la table `frais_forfait`
--
ALTER TABLE `frais_forfait`
  ADD PRIMARY KEY (`id_forfait`);

--
-- Index pour la table `frais_hors_forfait`
--
ALTER TABLE `frais_hors_forfait`
  ADD PRIMARY KEY (`id_Hforfait`),
  ADD KEY `frais_hors_forfait_ibfk_1` (`id_fiche_frais`);

--
-- Index pour la table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id_role`);

--
-- Index pour la table `type_frais`
--
ALTER TABLE `type_frais`
  ADD PRIMARY KEY (`id_type`);

--
-- Index pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  ADD PRIMARY KEY (`id_utilisateur`),
  ADD KEY `id_role` (`id_role`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `etat`
--
ALTER TABLE `etat`
  MODIFY `id_etat` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `fiche_de_frais`
--
ALTER TABLE `fiche_de_frais`
  MODIFY `id_fiche_frais` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT pour la table `frais_forfait`
--
ALTER TABLE `frais_forfait`
  MODIFY `id_forfait` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=53;

--
-- AUTO_INCREMENT pour la table `frais_hors_forfait`
--
ALTER TABLE `frais_hors_forfait`
  MODIFY `id_Hforfait` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT pour la table `role`
--
ALTER TABLE `role`
  MODIFY `id_role` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `type_frais`
--
ALTER TABLE `type_frais`
  MODIFY `id_type` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  MODIFY `id_utilisateur` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `fiche_de_frais`
--
ALTER TABLE `fiche_de_frais`
  ADD CONSTRAINT `fiche_de_frais_ibfk_1` FOREIGN KEY (`id_etat`) REFERENCES `etat` (`id_etat`),
  ADD CONSTRAINT `id_utilisateur_fk` FOREIGN KEY (`id_utilisateur`) REFERENCES `utilisateur` (`id_utilisateur`);

--
-- Contraintes pour la table `frais_hors_forfait`
--
ALTER TABLE `frais_hors_forfait`
  ADD CONSTRAINT `frais_hors_forfait_ibfk_1` FOREIGN KEY (`id_fiche_frais`) REFERENCES `fiche_de_frais` (`id_fiche_frais`);

--
-- Contraintes pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  ADD CONSTRAINT `utilisateur_ibfk_1` FOREIGN KEY (`id_role`) REFERENCES `role` (`id_role`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
